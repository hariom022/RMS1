using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;
using RMS.Models.Dto;
using RMS.Utility;
using System.Net;

namespace RMSWeb.Areas.User.Controllers
{
    [Area("User")]
    public class QuotationController : Controller
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuotationRepository _quotationRepo;
        private readonly ILogger<QuotationController> _logger;

        public QuotationController(IUnitOfWork unitOfWork, IQuotationRepository quotationRepo, IMapper mapper, ILogger<QuotationController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _response = new APIResponse();
            _logger = logger;
            _quotationRepo = quotationRepo;
        }

        public IActionResult QuotationIndex()
        {
            try
            {
                List<Quotation> quotationList = _unitOfWork.Quotation.GetAll().ToList();
                return View(quotationList);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while getting the product list.");

                // Return a generic error message
                return Content("An error occurred while getting the product list. Please try again later.");
            }

        }

        [HttpPost]
        public IActionResult QuotationIndexPost(int id, string status)
        {
            //need to send Proof of delivery to SAP API
            try
            {
                var quotation = _unitOfWork.Quotation.Get(u => u.Id == id);
                if (quotation != null)
                {
                    quotation.Status = status;
                    _unitOfWork.Quotation.Update(quotation);
                    _unitOfWork.Quotation.Save();
                }

                return RedirectToAction(nameof(QuotationIndex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured.");
                return Content("An error occurred. Please try again later.");
            }
        }


        #region Receive SAP Quotation API
        /// <summary>
        /// API : To receive the request for Quotation based on Order RequestNo
        /// </summary>
        /// <param name="createDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/QuotationAPI")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> CreateQuotation([FromBody] QuotationCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Quotation request cannot be null." };
                    return BadRequest(_response);
                }

                if (_unitOfWork.Quotation.Get(u => u.QuotationNo == createDTO.QuotationNo) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Quotation already exists." };
                    return BadRequest(_response);
                }

                // Validate that the RequestNo exists in the PurchaseOrderHeader table
                var purchaseOrderHeader = _unitOfWork.OrderHeader.Get(u => u.RequestNo == createDTO.RequestNo);

                if (purchaseOrderHeader == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "RequestNo does not exist." };
                    return BadRequest(_response);
                }

                purchaseOrderHeader.OrderStatus = SD.StatusApproved; //updating the Status of OrderHeader table
                _unitOfWork.OrderHeader.Update(purchaseOrderHeader);
                _unitOfWork.Quotation.Save();

                Quotation quotation = _mapper.Map<Quotation>(createDTO);
                // Set the PurchaseOrderHeaderId
                quotation.PurchaseOrderHeaderId = purchaseOrderHeader.Id;
                quotation.Status = SD.StatusPending;

                // Saving to DB
                _unitOfWork.Quotation.Add(quotation);
                _unitOfWork.Quotation.Save();


                var responseDTO = _mapper.Map<QuotationCreateDTO>(quotation);

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.Result = responseDTO;
                //return CreatedAtRoute("GetVilla", new { id = quotation.Id }, _response);
                return CreatedAtRoute(new { id = quotation.Id }, _response);
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
