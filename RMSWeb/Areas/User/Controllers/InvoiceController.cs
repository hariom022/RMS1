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
    public class InvoiceController : Controller
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;   
        private readonly ILogger<InvoiceController> _logger;
       

        [BindProperty]
        public InvoiceVM InvoiceVM { get; set; }
        public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper , ILogger<InvoiceController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _response = new APIResponse();
            _logger = logger;
           
        }


        public IActionResult InvoiceIndex()
        {
            try
            {
               List<Invoice> invoiceList= _unitOfWork.Invoice.GetAll().ToList();
                var distinctInvoice = invoiceList
                    .GroupBy(i => new { i.InvoiceNo, i.OBDNo, i.QuotationNo })
                    .Select(i => i.First())
                    .ToList();
                return View(distinctInvoice);
            }

            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while getting the invoice list.");

                // Return a generic error message
                return Content("An error occurred while getting the invoice list. Please try again later.");
            }
        }
        public IActionResult InvoiceDetails(string invoiceNo)
        {
            try
            {
                var invoiceList = _unitOfWork.Invoice.GetAll(i => i.InvoiceNo == invoiceNo).ToList();
                if (!invoiceList.Any())
                {
                    return NotFound();
                }
                return View(invoiceList);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching the details for Invoice with Id {InvoiceId}", invoiceNo);
                return Content("An error occurred while getting the Invoice list. Please try again later.");
            }
        }

        [HttpPost]
        public IActionResult InvoicePost(string invoiceNo, string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(invoiceNo) || string.IsNullOrWhiteSpace(status))
                {
                    return BadRequest("Invalid status value or invoice No");
                }
                var invoiceList = _unitOfWork.Invoice.GetAll(i => i.InvoiceNo == invoiceNo).ToList();

                foreach(var invoice in invoiceList)
                {
                    invoice.Status = status;
                    _unitOfWork.Invoice.Update(invoice);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(InvoiceIndex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating Invoice entities.");
                // Return a more informative error message or redirect to an error page
                return Content("An error occurred while updating Invoice entities. Please try again later.");
            }
        }

        #region Receive SAP Invoice API 
        /// <summary>
        /// API : To receive the invoice for Quotation based on Order OBDNo
        /// </summary>
        /// <param name="createDTOs"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("api/InvoiceAPI")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<APIResponse> CreatedInvoice([FromBody] List<InvoiceCreateDTO> createDTOs)
        {
            try
            {
                if(createDTOs == null || !createDTOs.Any())
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Invoice request cannot be null." };
                    return BadRequest(_response);
                }
                var firstDTO = createDTOs.First();

                if(_unitOfWork.Invoice.Get(i=>i.InvoiceNo == firstDTO.InvoiceNo) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "invoice already exists." };
                    return BadRequest(_response);
                }
                // Validate that the obdNo exists in the GoodIssue table
                var goodIssue = _unitOfWork.GoodsIssue.Get(u => u.OBDNo == firstDTO.OBDNo);

                if (goodIssue == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "OBD does not exist." };
                    return BadRequest(_response);
                }
                goodIssue.Status = SD.StatusApproved;  //updating the Status of OrderHeader table
                _unitOfWork.GoodsIssue.Update(goodIssue);
                _unitOfWork.GoodsIssue.Save();


                List<Invoice> invoices = new List<Invoice>();
                foreach(var createDTO in createDTOs)
                {
                    Invoice invoice = _mapper.Map<Invoice>(createDTO);
                    invoice.GoodsIssueId = goodIssue.Id;
                    invoice.Status = SD.StatusPending;
                    invoices.Add(invoice);
                    _unitOfWork.Invoice.Add(invoice);
                }
                _unitOfWork.Invoice.Save();

                var responseDTOs = invoices.Select(i => _mapper.Map<InvoiceCreateDTO>(i)).ToList();
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.Result = responseDTOs;
                return CreatedAtRoute(new { id = invoices.First().Id }, _response);
            }
            catch(Exception ex)
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
