using AlphaShop.Application.Common.DTOs;
using AlphaShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.Interfaces
{
    public interface IAdminUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAdminUsers();
        Task<bool> ToggleLock(string userId);
        Task<IdentityResult> CreateAdminUser(CreateAdminUserDto dto);

    }
}
