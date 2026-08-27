
using AlphaShop.Application.Common.DTOs;
using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Web.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AlphaShop.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAutService _autService;

        public AuthController(IAutService autService)
        {
            _autService = autService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var registerDTO = new RegisterDTO(registerVM.Name, registerVM.PhoneNumber, registerVM.Password);
                var result = await _autService.RegisterAsync(registerDTO);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(registerVM);
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var loginDTO = new LoginDTO(loginVM.PhoneNumber, loginVM.Password, loginVM.RemeberMe);
                var result = await _autService.LoginAsync(loginDTO);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "ورود ناموفق بود.");
                }
            }
            return View(loginVM);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
    }
}
