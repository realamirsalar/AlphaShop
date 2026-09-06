using AlphaShop.Application.Common.DTOs;
using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Common.Utitlity;
using AlphaShop.Web.ViewModels.AdminUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlphaShop.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminRole)]
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        public async Task<IActionResult> Index()
        {
            var adminUsers =await _adminUserService.GetAdminUsers();

            return View(adminUsers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleLock(string userId)
        {
            var result = await _adminUserService.ToggleLock(userId);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "خطایی رخ داد!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new CreateAdminUserVM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdminUserVM userVM)
        {
            if(ModelState.IsValid)
            {

                //var claimsIdentity = (ClaimsIdentity)HttpContext.User.Identity;
                //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await _adminUserService.CreateAdminUser(new CreateAdminUserDto
                {
                    Name = userVM.Name,
                    PhoneNumber = userVM.PhoneNumber,
                    Password = userVM.Password
                });
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(userVM);
        }
    }
}
