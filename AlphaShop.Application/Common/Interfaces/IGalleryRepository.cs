using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.Interfaces
{
    public interface IGalleryRepository : IRepository<Gallery>
    {
        void Create(Gallery gallery);
    }
}
