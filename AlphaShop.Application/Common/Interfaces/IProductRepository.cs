using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
        Product GetForProductDetail(int id);
        List<Product> GetByCategoryId(int categoryId);
        List<Product> GetProductForPageNation(int page, int pageSize);
    }
}
