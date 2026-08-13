using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Infrastructure.Contract.Repositories
{
    public class GalleryRepository : Repository<Gallery> , IGalleryRepository
    {
        private readonly ApplicationDbContext _db;

        public GalleryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Create(Gallery gallery)
        {
            _db.Galleries.Add(gallery);
        }
    }
}
