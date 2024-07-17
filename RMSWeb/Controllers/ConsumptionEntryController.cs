using Microsoft.AspNetCore.Mvc;
using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;

namespace RMSWeb.Controllers
{
    [Route("/Consumption/[action]")]
    public class ConsumptionEntryController : Controller
    {
        private readonly ApplicationDbContext _dbContex;
        private readonly IStoreStockRepository _storeStockRepository;
        private readonly IConsumptionEntryRepository _consumptionEntryRepository;

        public ConsumptionEntryController(ApplicationDbContext _dbContex, IStoreStockRepository storeStockRepository, IConsumptionEntryRepository consumptionEntryRepository)
        {
            this._dbContex = _dbContex;
            _storeStockRepository = storeStockRepository;
            _consumptionEntryRepository = consumptionEntryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }


        public IActionResult Create()
        {
            // Action to display the consumption entry form
            List<Store> plants = new()
            {
                 new Store { StoreId = 1, StoreName = "Store1", Location = "Location 1", Manager="Manager A", DataSource="Data Source A" , IsActive=true, CreatedBy="Create A" },
                new Store { StoreId = 2, StoreName = "Store2", Location = "Location 2", Manager = "Manager B", DataSource = "Data Source B", IsActive = true, CreatedBy = "Create B" },
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

        public IActionResult GetStockList()
        {
            try
            {

                var stocks = (from ss in _dbContex.StoreStocks
                              join p in _dbContex.Products on ss.ProductId equals p.ProductId
                              where ss.StoreId == 1
                              select new
                              {
                                  ss.ProductId,
                                  ss.Quantity,
                                  p.Number,
                                  p.Description
                              });

                return Json(new { isSuccess = true, stocks });
            }
            catch (Exception)
            {
                return Json(new { isSuccess = false });
            }
        }

        // Other action methods for updating, deleting, and viewing consumption entries
        public IActionResult UpdateStock(List<StockConsumption> lstConsumption)
        {
            int successCount = 0;
            try
            {
                //Step 1: Loop the consumption list
                //Step 2: For each Product check the stock in the table.
                //Step 3: If sufficient Stock found, update the entry in transacton. Else return msg

                lstConsumption.ForEach(x => { x.StoreId = 1; x.UserId = 1; });

                foreach (var consumption in lstConsumption)
                {
                    var storeStock = _storeStockRepository.GetStoreStock(consumption.StoreId, consumption.ProductId);
                    int availableStock = storeStock?.Quantity ?? 0;

                    if (availableStock >= consumption.ConsumedQty)
                    {
                        var consumptionEntry = new ConsumptionEntry()
                        {
                            UserId = consumption.UserId,
                            StoreId = consumption.StoreId,
                            ProductId = consumption.ProductId,
                            Quantity = consumption.ConsumedQty,
                            ConsumptionDate = DateOnly.FromDateTime(DateTime.Now),
                            DataSource = "WEB",
                            IsActive = true,
                            CreatedOn = DateTime.Now,
                            CreatedBy = ""
                        };

                        bool isSuccess = _consumptionEntryRepository.Save(consumptionEntry, storeStock);

                        if (isSuccess) { successCount++; }
                        else
                        {
                            return Json(new { isSucess = false, successCount, msg = "Error Occured" });
                        }
                    }
                    else
                    {
                        return Json(new { isSucess = false, successCount, msg = "Count Exceeded" });
                    }
                }
                return Json(new { isSuccess = true });
            }
            catch (Exception)
            {
                return Json(new { isSuccess = false, successCount, msg = "Error Occured" });
            }
        }
    }
}
