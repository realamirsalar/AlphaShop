using AlphaShop.Application.Common.DTOs;
using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Common.Utitlity;
using AlphaShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace AlphaShop.Infrastructure.Services
{
    public class AutService : IAutService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AutService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<bool> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.PhoneNumber);
            if (user == null)
            {
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user , loginDTO.Password , loginDTO.RememberMe , false);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDTO appUser)
        {
            var user = await _userManager.FindByNameAsync(appUser.PhoneNumber);
            if (user == null)
            {
                var applicationUser = new ApplicationUser()
                {
                    Name = appUser.Name,
                    PhoneNumber = appUser.PhoneNumber,
                    UserName = appUser.PhoneNumber,
                    CreateAt = DateTime.UtcNow,
                };
                var result = await _userManager.CreateAsync(applicationUser, appUser.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(applicationUser, SD.CustomerRole);
                }
                return result;
            }
            return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = "این کاربر قبلاً ثبت‌نام کرده است." } });
        }
    }
}
