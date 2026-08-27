using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Common.Utitlity;
using AlphaShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop.Infrastructure.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    await _db.Database.MigrateAsync();
                }
            }
            catch
            {
                throw;
            }

            if (!await _roleManager.RoleExistsAsync(SD.AdminRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.AdminRole));
            }

            if (!await _roleManager.RoleExistsAsync(SD.CustomerRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.CustomerRole));
            }

            var user = await _userManager.FindByNameAsync("09000000000");
            if (user == null)
            {
                ApplicationUser adminUser = new ApplicationUser()
                {
                    UserName = "09000000000",
                    PhoneNumber = "09000000000",
                    CreateAt = DateTime.Now,
                    Name = "AdminUser",
                    Email = "Admin@gmail.com"
                };
                var result = await _userManager.CreateAsync(adminUser, "Admin123*");
                await _userManager.AddToRoleAsync(adminUser, SD.AdminRole);
            }
        }

    }
}
