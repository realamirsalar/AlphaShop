using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Common.Utitlity;
using AlphaShop.Application.Services.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Web.ViewModels.Category;
using AlphaShop.Web.ViewModels.Product;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlphaShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        private readonly IGalleryService _galleryService;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public ProductController(ICategoryService categoryService, IProductService productService, IFileService fileService, IWebHostEnvironment environment, IMapper mapper, IGalleryService galleryService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _fileService = fileService;
            _environment = environment;
            _mapper = mapper;
            _galleryService = galleryService;
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

                    if(imageUrl == null)
                    {
                        ModelState.AddModelError("Image", "لطفا عکسی با فرمت درست وارد کنید.!");
                        productVM.Categories = _categoryService.GetChildeCategories().Select(c => new SelectListItem()
                        {
                            Text = c.Name,
                            Value = c.Id.ToString()
                        });
                        return View(productVM);
                    }
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
                    return RedirectToAction(nameof(Gallery) , new { id = product.Id});

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
        public IActionResult Gallery(int id)
        {
            var product = _productService.GetById(id);
            product.Galleries = _galleryService.GetByProductId(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Gallery(int id,IFormFile FileUpload)
        {
            var product = _productService.GetById(id); 
            if (FileUpload.Length == 0 || FileUpload == null)
            {
                ModelState.AddModelError("", "لطفا عکسی وارد کنید");
                product.Galleries = _galleryService.GetByProductId(id);
                return View(product);
            }
            var rootPath = _environment.WebRootPath;
            var fileName = FileUpload.FileName;
            var streamFile = FileUpload.OpenReadStream();
            var imageUrl = _fileService.SaveFile(rootPath, SD.ProductLocation, fileName, streamFile);
            if (imageUrl == null)
            {
                ModelState.AddModelError("FileUpload", "لطفا عکسی با فرمت درست وارد کنید.!");
                return View(product);
            }
            var gallery = new Gallery()
            {
                ImageUrl = imageUrl,
                ProductId = product.Id
            };
            _galleryService.Create(gallery);
            TempData["success"] = "عکس با موفقیت افزوده شد!";
            product.Galleries = _galleryService.GetByProductId(id);
            return View(product);
        }
        
        public IActionResult DeleteGallery(int id)
        {
            var gallery = _galleryService.Get(id);
            if (gallery == null)
            {
                TempData["error"] = "عکس مورد نظر پیدا نشد!";
                return RedirectToAction(nameof(Gallery), new { id = gallery.ProductId });
            }
            var rootPath = _environment.WebRootPath;
            _galleryService.DeleteGallery(gallery.Id);
            _fileService.DeleteFile(rootPath, gallery.ImageUrl.TrimStart('\\'));
            TempData["success"] = "عکس با موفقیت حذف شد!";
            return RedirectToAction(nameof(Gallery), new { id = gallery.ProductId });
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
                    if (imageUrl == null)
                    {
                        ModelState.AddModelError("Image", "لطفا عکسی با فرمت درست وارد کنید.!");
                        productVM.Categories = _categoryService.GetChildeCategories().Select(c => new SelectListItem()
                        {
                            Text = c.Name,
                            Value = c.Id.ToString()
                        });
                        return View(productVM);
                    }
                    _mapper.Map(productVM, product);
                    product.ImageUrl = imageUrl;

                    _productService.UpdateProduct(product);

                    _fileService.DeleteFile(rootPath, oldUrl);

                    TempData["success"] = "محصول با موفقیت ویرایش شد!";
                    return RedirectToAction(nameof(Gallery), new { id = product.Id });

                }
                else
                {
                    _mapper.Map(productVM, product);
                    _productService.UpdateProduct(product);
                    TempData["success"] = "محصول با موفقیت ویرایش شد!";
                    return RedirectToAction(nameof(Gallery), new { id = product.Id });
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
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                TempData["error"] = "محصول مورد نظر پیدا نشد!";
                return RedirectToAction(nameof(Index));
            }
            var productVM = _mapper.Map<ProductVM>(product);

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ProductVM productVM)
        {
            //    if (ModelState.IsValid)
            //    {
            var imageUrl = _productService.DeleteProduct(productVM.Id);
            if (imageUrl!= null)
            {
                var rootPath = _environment.WebRootPath;
                _fileService.DeleteFile(rootPath, imageUrl);
                TempData["success"] = "محصول با موفقیت حذف شد!";
            }
            else
            {
                TempData["error"] = "محصول مورد نظر پیدا نشد!";
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
