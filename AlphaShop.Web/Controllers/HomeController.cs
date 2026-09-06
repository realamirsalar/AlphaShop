using AlphaShop.Application.Services.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AlphaShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Products(int page = 1)
        {
            var allProducts = _productService.GetAllProducts();
            var productDto = _productService.PageNation(page, 4);
            ViewBag.PageCount = productDto.PageCount;
            ViewBag.Page = page;
            return View(productDto.Products);
        }
        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetByIdForDetails(id);
            if (product == null)
            {
                return RedirectToAction("Products");
            }
            //if (product.NotShow)
            //{
            //    return RedirectToAction("Products");
            //}
            return View(product);
        }
    }
}
