using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Infrastructure.Contract.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public List<Product> GetProductForPageNation(int page, int pageSize)
        {
            if (page <= 0)
            {
                return _db.Products.Where(p => p.NotShow == false).Take(pageSize).ToList();
            }
            return _db.Products.Where(p => p.NotShow == false).Skip(((int)page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Update(Product product)
        {
            _db.Update(product);
        }
    }
}
