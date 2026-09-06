using AlphaShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlphaShop.Web.ViewComponents
{
    public class ProductDetailViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductDetailViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var product = _productService.GetProductsByCategoryIdForDetail(id);
           
            return View(product);
        }

    }
}
