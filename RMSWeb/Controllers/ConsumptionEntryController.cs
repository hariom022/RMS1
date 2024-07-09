using Microsoft.AspNetCore.Mvc;
using RMS.Models;

namespace RMSWeb.Controllers
{
    [Route("/Consumption/[action]")]
    public class ConsumptionEntryController : Controller
    {
        public IActionResult Index()
        {
            // Action to display a list of consumption entries

            return View();
        }


        public IActionResult Create()
        {
            // Action to display the consumption entry form
            List<StoreMaster> plants = new()
            {
                 new StoreMaster { StoreId = 1, StoreName = "Store1", Location = "Location 1", Manager="Manager A", DataSource="Data Source A" , IsActive=true, CreatedBy="Create A" },
                new StoreMaster { StoreId = 2, StoreName = "Store2", Location = "Location 2", Manager = "Manager B", DataSource = "Data Source B", IsActive = true, CreatedBy = "Create B" },
                // Add more plants as needed
            };
            return View(plants);
        }

        [HttpPost]
        public IActionResult Create(ConsumptionEntry newEntry)
        {
            // Action to process the form submission and save the consumption entry
            return RedirectToAction("Index");
        }

        // Other action methods for updating, deleting, and viewing consumption entries
    }
}
