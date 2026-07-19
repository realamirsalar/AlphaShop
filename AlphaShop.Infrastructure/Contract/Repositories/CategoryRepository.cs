using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Infrastructure.Contract.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateCateory(Category category)
        {
            _db.Categories.Add(category);
        }
    }
}
