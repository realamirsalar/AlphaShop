using AlphaShop.Application.Common.DTOs;
using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        List<Product> GetAllIsShowProducts();
        PageNationDto PageNation(int page, int pageSize);
        Product GetById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        string DeleteProduct(int id);
    }
}
