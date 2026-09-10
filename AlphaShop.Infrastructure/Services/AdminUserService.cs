using AlphaShop.Application.Common.DTOs;
using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Common.Utitlity;
using AlphaShop.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AlphaShop.Infrastructure.Services
{
    public class AdminUserService : IAdminUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public AdminUserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<IdentityResult> CreateAdminUser(CreateAdminUserDto dto)
        {
            var adminUser = new ApplicationUser
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                UserName = dto.PhoneNumber,
            };
            var appUser = await _userManager.CreateAsync(adminUser, dto.Password);

            if (appUser.Succeeded)
            {
                var result = await _userManager.AddToRoleAsync(adminUser, SD.AdminRole);
                return result;
            }
            return appUser;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAdminUsers()
        {
            return await _userManager.GetUsersInRoleAsync(SD.AdminRole);
        }

        public async Task<bool> ToggleLock(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;

            }

            var currentUserId = _contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUserId == userId)
            {
                return false;
            }
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.UtcNow)
            {
                user.LockoutEnd = null;
            }
            else
            {
                user.LockoutEnd = DateTime.UtcNow.AddYears(100);
            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
