using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Services.Interfaces
{
    public interface IGalleryService
    {
        void Create(Gallery gallery);
        List<Gallery> GetByProductId(int productId);
        Gallery Get(int id);
        void DeleteGallery(int id);
    }
}
