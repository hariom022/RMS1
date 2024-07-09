using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;
using RMS.Models.ViewModels;
using RMS.Utility;
using RMSWeb.Services.IServices;
using System.Security.Claims;

namespace RMSWeb.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IPurchaseOrderCartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISapService _sapService;
        private readonly IProductMasterRepository _productRepository;
        private readonly ApplicationDbContext applicationDb;

        [BindProperty]
        public PurchaseOrderCartVM PurchaseOrderCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork, IPurchaseOrderCartRepository cartRepository, ISapService sapService, IProductMasterRepository productRepository , ApplicationDbContext applicationDb)
        {
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
            _sapService = sapService;
            _productRepository = productRepository;
            this.applicationDb = applicationDb;
        }

        public IActionResult Index()
        {
            //getting UserId
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<ProductMaster> product = _productRepository.GetAll().ToList();
            PurchaseOrderCartVM = new()
            {
                PurchaseOrderCartList = _cartRepository.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                PurchaseOrderHeader = new(),
                Products = product

            };
            

            return View(PurchaseOrderCartVM);
        }

        //-------------------------------------Add Product Through Cart----------------------------------------

        [HttpPost]
        public IActionResult AddToCart(PurchaseOrderCartVM model)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (model.SelectedProductId != 0 && model.QuantityToAdd > 0)
            {
                var product = _productRepository.Get(p => p.ProductId == model.SelectedProductId);

                if (product != null)
                {
                    var cartItem = new PurchaseOrderCart
                    {
                        ApplicationUserId = userId,
                        ProductId = product.ProductId,
                        Count = model.QuantityToAdd
                    };

                    _cartRepository.Add(cartItem);
                    _cartRepository.Save();
                }
            }

            return RedirectToAction(nameof(Index));
        }
        //--------------------------------------------------------------------



        private string GenerateRequestNo()
        {
            // Logic to generate the next request number
            var lastRequestNo = applicationDb.PurchaseOrderHeaders
                .OrderByDescending(p => p.Id)
                .Select(p => p.RequestNo)
                .FirstOrDefault();

            int nextRequestNo = 1000000001;
            if (lastRequestNo != null && int.TryParse(lastRequestNo, out int lastNo))
            {
                nextRequestNo = lastNo + 1;
            }

            return nextRequestNo.ToString("D10");
        }
        public IActionResult OrderSummary()
        {
            //getting UserId
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            PurchaseOrderCartVM = new()
            {
                PurchaseOrderCartList = _cartRepository.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                PurchaseOrderHeader = new()
            };

            PurchaseOrderCartVM.PurchaseOrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            var storeId = PurchaseOrderCartVM.PurchaseOrderHeader.ApplicationUser.StoreId;
            PurchaseOrderCartVM.PurchaseOrderHeader.Store = _unitOfWork.Store.Get(u => u.StoreId == storeId).StoreName;

            // Generate RequestNo and set it to the PurchaseOrderHeader
            PurchaseOrderCartVM.PurchaseOrderHeader.RequestNo = GenerateRequestNo();

            //foreach (var cart in PurchaseOrderCartVM.PurchaseOrderCartList)
            //{

            //}

            return View(PurchaseOrderCartVM);
        }
        


        [HttpPost]
        [ActionName("OrderSummary")]
        public IActionResult OrderSummaryPost()
        {
            //getting UserId
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            PurchaseOrderCartVM.PurchaseOrderCartList = _cartRepository.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product");

            PurchaseOrderCartVM.PurchaseOrderHeader.ApplicationUserId = userId;
            PurchaseOrderCartVM.PurchaseOrderHeader.RequestNo = GenerateRequestNo();
            //if(InquiryAPI is success) - see below method
            PurchaseOrderCartVM.PurchaseOrderHeader.OrderStatus = SD.StatusInProcess;

            _unitOfWork.OrderHeader.Add(PurchaseOrderCartVM.PurchaseOrderHeader);
            _unitOfWork.Save();

            foreach (var cart in PurchaseOrderCartVM.PurchaseOrderCartList)
            {
                PurchaseOrderItem orderItem = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = PurchaseOrderCartVM.PurchaseOrderHeader.Id,
                    Count = cart.Count
                };
                _unitOfWork.OrderItem.Add(orderItem);
                _unitOfWork.Save();
            }


            return RedirectToAction(nameof(OrderConfirmation), new { id = PurchaseOrderCartVM.PurchaseOrderHeader.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitInquiry(PurchaseOrderCartVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _sapService.PostInquiryRequestAsync<APIResponse>(model);
                if (response != null || response.IsSuccess)
                {
                    //create logic to save it in the database. - need to do.


                    return RedirectToAction(nameof(OrderSummary));
                }
            }
            return View(model);
        }

        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _cartRepository.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _cartRepository.Update(cartFromDb);
            _cartRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _cartRepository.Get(u => u.Id == cartId);
            if (cartFromDb.Count <= 1)
            {
                //remove from cart
                _cartRepository.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _cartRepository.Update(cartFromDb);
            }

            _cartRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _cartRepository.Get(u => u.Id == cartId);

            _cartRepository.Remove(cartFromDb);

            _cartRepository.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
