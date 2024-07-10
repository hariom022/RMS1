using Microsoft.AspNetCore.Mvc;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;

namespace RMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class StoreController : Controller
    {
        private readonly IStoreRepository _plantRepo;

        public StoreController(IStoreRepository plantRepo)
        {
            _plantRepo = plantRepo;
        }

        public IActionResult Index()
        {
            List<Store> storeList = _plantRepo.GetAll().ToList();
            return View(storeList);
        }


        //public IActionResult Details(string id)
        //{
        //    Plant plant = GetPlantById(id);

        //    if (plant == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(plant);
        //}

        //private Plant GetPlantById(string id)
        //{
        //    // Dummy implementation - Replace with actual data retrieval logic
        //    return new Plant { PlantName = id, PlantDescription = "Description for Plant " + id };
        //}

        #region API CALLS

        //GetALL()

        //Delete


        #endregion
    }
}
