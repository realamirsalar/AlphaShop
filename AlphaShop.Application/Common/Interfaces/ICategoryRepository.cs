using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.Interfaces
{
    public interface ICategoryRepository
    {
        void CreateCateory(Category category);
        List<Category> GetAllCategories();
        List<Category> GetParsntCategories();
        List<Category> GetChildeCategories();
        Category GetCategory(int id);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
