using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Infrastructure.Contract;
using AlphaShop.Infrastructure.Contract.Repositories;
using AlphaShop.Infrastructure.Data;
using AlphaShop.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructuerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IFileService, FileService>();
            return services;
        }
    }
}
