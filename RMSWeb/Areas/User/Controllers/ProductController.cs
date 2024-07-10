using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;
using System.Security.Claims;

namespace RMSWeb.Areas.User.Controllers
{
    [Area("User")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IPurchaseOrderCartRepository _cartRepo;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductRepository productRepo, IPurchaseOrderCartRepository cartRepo, ILogger<ProductController> logger)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                List<Product> productList = _productRepo.GetAll().ToList();
                return View(productList);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while getting the product list.");

                // Return a generic error message
                return Content("An error occurred while getting the product list. Please try again later.");
            }

        }

        public IActionResult Details(int productId)
        {
            try
            {
                PurchaseOrderCart orderCart = new()
                {
                    Product = _productRepo.Get(u => u.ProductId == productId),
                    Count = 1,
                    ProductId = productId
                };

                return View(orderCart);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while getting the product details list.");

                // Return a generic error message
                return Content("An error occurred while getting the product list. Please try again later.");
            }

        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(PurchaseOrderCart orderCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            orderCart.ApplicationUserId = userId;

            PurchaseOrderCart cartFromDb = _cartRepo.Get(u => u.ApplicationUserId == userId && u.ProductId == orderCart.ProductId);

            if (cartFromDb != null)
            {
                //order cart exists
                cartFromDb.Count += orderCart.Count;
                _cartRepo.Update(cartFromDb);
            }
            else
            {
                //add order in cart
                _cartRepo.Add(orderCart);
            }

            TempData["success"] = "Order cart updated successfully";
            _cartRepo.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
