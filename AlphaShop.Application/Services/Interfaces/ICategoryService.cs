using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        void CreateCategory(Category category);
        List<Category> GetAllCategories();
        List<Category> GetParntCategories();
        Category GetByIdCategory(int id);

        void UpdateCategory(Category category);
        bool DeleteCategory(int id);
    }
}
