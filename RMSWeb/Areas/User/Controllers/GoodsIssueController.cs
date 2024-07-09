using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;
using RMS.Models.Dto;
using RMS.Models.ViewModels;
using RMS.Utility;
using System.Net;

namespace RMSWeb.Areas.User.Controllers
{
    [Area("User")]
    public class GoodsIssueController : Controller
    {

        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GoodsIssueController> _logger;

        [BindProperty]
        public GoodsIssueVM GoodsIssueVM { get; set; }
        public GoodsIssueController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GoodsIssueController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _response = new APIResponse();
            _logger = logger;
        }

        public IActionResult GoodsIssueIndex()
        {
            try
            {

                List<GoodsIssue> goodsIssuesList = _unitOfWork.GoodsIssue.GetAll().ToList();

                var distinctGoodsIssues = goodsIssuesList
                    .GroupBy(g => new { g.QuotationNo, g.OBDNo })
                    .Select(g => g.First())
                    .ToList();

                return View(distinctGoodsIssues);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while getting the product list.");

                // Return a generic error message
                return Content("An error occurred while getting the product list. Please try again later.");
            }
        }

        public IActionResult GoodsIssueDetails(string obdNo)
        {
            try
            {
                var goodsIssuesList = _unitOfWork.GoodsIssue.GetAll(g => g.OBDNo == obdNo).ToList();

                if (!goodsIssuesList.Any())
                {
                    return NotFound();
                }

                return View(goodsIssuesList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching the details for GoodsIssue with Id {GoodsIssueId}", obdNo);
                return Content("An error occurred while getting the product list. Please try again later.");
            }


        }


        [HttpPost]
        public IActionResult GoodsIssuePost(string obdNo, string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(obdNo) || string.IsNullOrWhiteSpace(status))
                {
                    return BadRequest("Invalid status value or Obd No");
                }

                var goodsIssueList = _unitOfWork.GoodsIssue.GetAll(g => g.OBDNo == obdNo).ToList();

                foreach (var goodsIssue in goodsIssueList)
                {
                    goodsIssue.Status = status;
                    _unitOfWork.GoodsIssue.Update(goodsIssue);
                }

                _unitOfWork.Save();

                return RedirectToAction(nameof(GoodsIssueIndex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating GoodsIssue entities.");
                // Return a more informative error message or redirect to an error page
                return Content("An error occurred while updating GoodsIssue entities. Please try again later.");
            }
        }


        #region Receive SAP GoodsIssue API 
        /// <summary>
        /// API : To receive the request for Quotation based on Order RequestNo
        /// </summary>
        /// <param name="createDTOs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/GoodsIssueAPI")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> CreateGoodsIssue([FromBody] List<GoodsIssueCreateDTO> createDTOs)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    _response.IsSuccess = false;
                //    _response.ErrorMessages = errors;
                //    return BadRequest(_response);
                //}

                if (createDTOs == null || !createDTOs.Any())
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Goods Issue request cannot be null." };
                    return BadRequest(_response);
                }
                // Assuming that all DTOs in the request will have the same QuotationNo and OBDNo.
                var firstDTO = createDTOs.First();

                if (_unitOfWork.GoodsIssue.Get(u => u.OBDNo == firstDTO.OBDNo) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Goods Issue already exists." };
                    return BadRequest(_response);
                }

                // Validate that the RequestNo exists in the PurchaseOrderHeader table
                var quotation = _unitOfWork.Quotation.Get(u => u.QuotationNo == firstDTO.QuotationNo);

                if (quotation == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "QuotationNo does not exist." };
                    return BadRequest(_response);
                }

                quotation.Status = SD.StatusApproved; //updating the Status of OrderHeader table
                _unitOfWork.Quotation.Update(quotation);
                _unitOfWork.Quotation.Save();

                List<GoodsIssue> goodsIssues = new List<GoodsIssue>();
                foreach (var createDTO in createDTOs)
                {
                    GoodsIssue goodsIssue = _mapper.Map<GoodsIssue>(createDTO);
                    goodsIssue.QuotationId = quotation.Id;
                    goodsIssue.Status = SD.StatusPending;
                    goodsIssues.Add(goodsIssue);
                    _unitOfWork.GoodsIssue.Add(goodsIssue);
                }

                _unitOfWork.GoodsIssue.Save();

                var responseDTOs = goodsIssues.Select(g => _mapper.Map<GoodsIssueCreateDTO>(g)).ToList();

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.Result = responseDTOs;
                return CreatedAtRoute(new { id = goodsIssues.First().Id }, _response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }

        #endregion
    }
}
