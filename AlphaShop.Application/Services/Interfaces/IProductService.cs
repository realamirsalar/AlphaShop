using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
    }
}
