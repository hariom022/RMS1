using Microsoft.AspNetCore.Mvc;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;

namespace RMSWeb.Areas.User.Controllers
{
    public class StoreStockController : Controller
    {
        private readonly IStoreStockRepository _stockRepository;
        private readonly Logger<StoreStock> _logger;

        public StoreStockController(IStoreStockRepository stockRepository, Logger<StoreStock> logger)
        {
            _stockRepository = stockRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            try
            {
                var stockList = _stockRepository.GetAll();

                return View(stockList);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error Occur while fetching the StockList");
                return Content("An error Occur while fetching the Stock List . Please try again ");

            }
         
        }
    }
}
