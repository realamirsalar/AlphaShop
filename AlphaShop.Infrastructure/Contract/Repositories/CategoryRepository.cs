using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public void DeleteCategory(Category category)
        {
            _db.Categories.Remove(category);
        }

        public List<Category> GetAllCategories()
        {
            return _db.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _db.Categories.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public List<Category> GetParsntCategories()
        {
            return _db.Categories.AsNoTracking().Where(c => c.ParentId == 0 || c.ParentId == null).ToList();
        }

        public void UpdateCategory(Category category)
        {
            _db.Update(category);
        }
    }
}
