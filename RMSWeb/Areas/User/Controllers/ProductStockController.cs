using Microsoft.AspNetCore.Mvc;

namespace RMSWeb.Areas.User.Controllers
{
    public class ProductStockController : Controller
    {
        public ProductStockController()
        {
             
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
