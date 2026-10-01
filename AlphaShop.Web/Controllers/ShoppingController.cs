using AlphaShop.Application.Common.DTOs;
using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Services.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Web.ViewModels.Shopping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlphaShop.Web.Controllers
{
    [Authorize]
    public class ShoppingController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        private readonly IProductService _productService;

        public ShoppingController(IShoppingCartService shoppingCartService, IProductService productService)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)HttpContext.User.Identity;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var carts = _shoppingCartService.GetAllShoppingCarts(userId);
            return View(carts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int productId,int quntity)
        {
            var claimsIdentity = (ClaimsIdentity)HttpContext.User.Identity;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = _productService.GetById(productId);
            if(product == null)
            {
                return RedirectToAction(nameof(NotFound));
            }
            if (product.Quntity <= quntity)
            {
                ModelState.AddModelError(string.Empty, "موجودی معتبر نیست");
                return RedirectToAction("ProductDetails" , "Home" , new {id = productId});
            }
            var cart = new ShoppingCart()
            {
                ApplicationUserId = userId,
                ProductId = productId,
                Count = quntity
            };
            _shoppingCartService.AddShoppingCart(cart);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int cartId)
        {
            var result = _shoppingCartService.RemoveShoppingCart(cartId);
            if (!result)
            {
                return RedirectToAction(nameof(NotFound));
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Plus(int cartId)
        {
            var result = _shoppingCartService.Plus(cartId);
            if (result)
            {

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(NotFound));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Minus(int cartId)
        {
            var result = _shoppingCartService.Minus(cartId);
            if (result)
            {

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(NotFound));
        }
       
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
