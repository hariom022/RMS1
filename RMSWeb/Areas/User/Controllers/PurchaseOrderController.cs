using Microsoft.AspNetCore.Mvc;
using RMS.Models;
using RMS.Models.Dto;
using RMSWeb.Services.IServices;

namespace RMSWeb.Areas.User.Controllers
{
    [Area("User")]
    public class PurchaseOrderController : Controller
    {
        private readonly ISapService _sapService;

        public PurchaseOrderController(ISapService sapService)
        {
            _sapService = sapService;
        }

        public IActionResult PurchaseOrder()
        {
            return View();
        }

        public IActionResult PurchaseOrderRequest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitInquiry(SalesInquiryRequestDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _sapService.PostInquiryRequestAsync<APIResponse>(model);
                if (response != null || response.IsSuccess)
                {
                    //create logic to save it in the database. - need to do.


                    return RedirectToAction(nameof(PurchaseOrder));
                }
            }
            return View(model);
        }
    }
}
