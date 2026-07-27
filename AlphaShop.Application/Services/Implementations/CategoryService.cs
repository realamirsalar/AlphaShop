using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Services.Interfaces;
using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateCategory(Category category)
        {
            _categoryRepository.CreateCateory(category);
            _unitOfWork.Save();
        }

        public bool DeleteCategory(int id)
        {
            var category = GetByIdCategory(id);
            if (category == null)
            {
                return false;
            }
            _categoryRepository.DeleteCategory(category);
            _unitOfWork.Save();
            return true;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public Category GetByIdCategory(int id)
        {
            return _categoryRepository.GetCategory(id);
        }

        public List<Category> GetParntCategories()
        {
            return _categoryRepository.GetParsntCategories();
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.UpdateCategory(category);
            _unitOfWork.Save();
        }
    }
}
