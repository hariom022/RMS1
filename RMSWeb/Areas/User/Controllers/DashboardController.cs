using Microsoft.AspNetCore.Mvc;

namespace RMSWeb.Areas.User.Controllers
{
    [Area("User")]
    public class DashboardController : Controller
    {
        //[Route("Dashboard")]
        public IActionResult Overview()
        {
            return View();
        }
    }
}
