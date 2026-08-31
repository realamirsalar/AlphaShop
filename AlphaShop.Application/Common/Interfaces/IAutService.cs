using AlphaShop.Application.Common.DTOs;
using Microsoft.AspNetCore.Identity;

namespace AlphaShop.Application.Common.Interfaces
{
    public interface IAutService
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO user);
        Task<string> LoginAsync(LoginDTO loginDTO);
        Task LogoutAsync();
    }
}
