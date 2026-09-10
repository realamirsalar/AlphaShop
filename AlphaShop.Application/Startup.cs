using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Services.Interfaces;
using AlphaShop.Application.Services.Implementations;

namespace AlphaShop.Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            return services;
        }
    }
}
