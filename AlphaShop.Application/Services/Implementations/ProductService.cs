using AlphaShop.Application.Common.DTOs;
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

        public List<Product> GetAllIsShowProducts()
        {
            return _productRepository.GetAll(p => p.NotShow == false).ToList();
        }

        public PageNationDto PageNation(int page, int pageSize)
        {
            var product = GetAllIsShowProducts();
            double pageCount = Math.Ceiling(product.Count() / (double) pageSize);
            int skipCount = (page - 1) * pageSize;
            product = _productRepository.GetProductForPageNation(page , pageSize);
            
            return new PageNationDto
            {
                Products = product,
                PageCount = (int)pageCount
            };
        }

        public Product GetByIdForDetails(int id)
        {
            var product = _productRepository.GetForProductDetail(id);
            if (product == null || product.NotShow == true)
            {
                return null;
            }
            product.Seen++;
            _unitOfWork.Save();
            return product;
        }

        public List<Product> GetProductsByCategoryIdForDetail(int categoryId)
        {
            return _productRepository.GetByCategoryId(categoryId).Take(4).ToList();
        }
    }
}
