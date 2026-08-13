using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Services.Interfaces;
using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateProduct(Product product)
        {
            _productRepository.Add(product);
            _unitOfWork.Save();
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product GetById(int id)
        {
            return _productRepository.Get(c => c.Id == id,includeProperties: "Category");
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            _unitOfWork.Save();
        }
        public string DeleteProduct(int id)
        {
            var product = GetById(id);
            if (product == null)
            {
                return null;
            }
            _productRepository.Remove(product);
            _unitOfWork.Save();
            return product.ImageUrl.TrimStart('\\'); 
        }
    }
}
