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
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AutService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.PhoneNumber);
            if (user == null)
            {
                return null;
            }
            //var lockDate = await _userManager.GetLockoutEndDateAsync(user);
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.UtcNow)
            {
                return SD.LockedOut;
            }
            var result = await _signInManager.PasswordSignInAsync(user , loginDTO.Password , loginDTO.RememberMe , false);
            
            if (result.Succeeded)
            {
                var role = await _userManager.GetRolesAsync(user);
                return role[0];
            }
            
            return null;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
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
