using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Common.Utitlity;
using AlphaShop.Application.Services.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Web.ViewModels.Product;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlphaShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public ProductController(ICategoryService categoryService, IProductService productService, IFileService fileService, IWebHostEnvironment environment, IMapper mapper)
        {
            _categoryService = categoryService;
            _productService = productService;
            _fileService = fileService;
            _environment = environment;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var productVM = new ProductVM
            {
                Categories = _categoryService.GetChildeCategories().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                if (productVM.Image.Length != 0 || productVM.Image != null)
                {
                    var rootPath = _environment.WebRootPath;
                    var fileName = productVM.Image.FileName;
                    var streamFile = productVM.Image.OpenReadStream();
                    var imageUrl = _fileService.SaveFile(rootPath, SD.ProductLocation, fileName, streamFile);

                    var product = new Product
                    {
                        Name = productVM.Name,
                        Description = productVM.Description,
                        Quntity = productVM.Quntity,
                        ImageUrl = imageUrl,
                        NotShow = productVM.NotShow,
                        OldPrice = productVM.OldPrice,
                        Price = productVM.Price,
                        RelaseDate = DateTime.UtcNow,
                        Tags = productVM.Tags,
                        CategoryId = productVM.CategoryId,
                    };
                    _productService.CreateProduct(product);

                    TempData["success"] = "محصول با موفقیت افزوده شد!";
                    return RedirectToAction(nameof(Index));

                }
            }
            productVM.Categories = _categoryService.GetChildeCategories().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(productVM);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var productVM = _mapper.Map<ProductVM>(product);

            productVM.Categories = _categoryService.GetChildeCategories().Select(c => new SelectListItem()
               {
                   Text = c.Name,
                   Value = c.Id.ToString()
               });

            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProductVM productVM)
        {
            var product = _productService.GetById(productVM.Id);
            if (ModelState.IsValid)
            {
                
                if (productVM.Image != null)
                {
                    var rootPath = _environment.WebRootPath;
                    var fileName = productVM.Image.FileName;
                    var streamFile = productVM.Image.OpenReadStream();
                    var imageUrl = _fileService.SaveFile(rootPath, SD.ProductLocation, fileName, streamFile);
                    var oldUrl = product.ImageUrl.TrimStart('\\'); 
                    
                    _mapper.Map(productVM, product);
                    product.ImageUrl = imageUrl;
                   
                    _productService.UpdateProduct(product);

                    _fileService.DeleteFile(rootPath, oldUrl);

                    TempData["success"] = "محصول با موفقیت ویرایش شد!";
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    _mapper.Map(productVM, product);
                    _productService.UpdateProduct(product);
                    TempData["success"] = "محصول با موفقیت ویرایش شد!";
                    return RedirectToAction(nameof(Index));
                }
            }

            productVM = _mapper.Map<ProductVM>(product);

            productVM.Categories = _categoryService.GetChildeCategories().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(productVM);
        }
        
    }
}
