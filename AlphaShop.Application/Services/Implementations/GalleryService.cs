using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Services.Interfaces;
using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Services.Implementations
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GalleryService(IUnitOfWork unitOfWork, IGalleryRepository galleryRepository)
        {
            _unitOfWork = unitOfWork;
            _galleryRepository = galleryRepository;
        }

        public void Create(Gallery gallery)
        {
            _galleryRepository.Create(gallery);
            _unitOfWork.Save();
        }

        public void DeleteGallery(int id)
        {
            _galleryRepository.Remove(_galleryRepository.Get(g => g.Id == id));
            _unitOfWork.Save();
        }

        public Gallery Get(int id)
        {
            return _galleryRepository.Get(g => g.Id == id);
        }

        public List<Gallery> GetByProductId(int productId)
        {
            return _galleryRepository.GetAll(g => g.ProductId == productId).ToList();
        }
    }
}
