using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Services.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Web.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlphaShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategories());
        }
        [HttpGet]
        public IActionResult Create()
        {
            var categoryVm = new CategoryVM();
            categoryVm.ParentLists = _categoryService.GetParntCategories().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(categoryVm);
        }

        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                Category category = new()
                {
                    Name = categoryVM.Name,
                    DisplayOrder = categoryVM.DisplayOrder,
                    ParentId = categoryVM.ParentId
                };
                _categoryService.CreateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            categoryVM.ParentLists = _categoryService.GetParntCategories().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(categoryVM);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _categoryService.GetByIdCategory(id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var categoryVm = new CategoryVM()
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder,
                ParentId = category.ParentId
            };

            categoryVm.ParentLists = _categoryService.GetParntCategories().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(categoryVm);
        }

        [HttpPost]
        public IActionResult Update(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                Category category = new()
                {
                    Id = categoryVM.Id,
                    Name = categoryVM.Name,
                    DisplayOrder = categoryVM.DisplayOrder,
                    ParentId = categoryVM.ParentId
                };
                _categoryService.UpdateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            categoryVM.ParentLists = _categoryService.GetParntCategories().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(categoryVM);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetByIdCategory(id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var categoryVm = new CategoryVM()
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder,
                ParentId = category.ParentId
            };

            return View(categoryVm);
        }

        [HttpPost]
        public IActionResult Delete(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.DeleteCategory(categoryVM.Id);
                if (result)
                {

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(categoryVM);
        }
        
    }
}
