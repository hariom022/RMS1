using Microsoft.AspNetCore.Mvc;

namespace RMSWeb.Areas.User.Controllers
{
    public class ProductStockMasterController : Controller
    {
        public ProductStockMasterController()
        {
             
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
