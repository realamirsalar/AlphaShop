using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Infrastructure.Contract.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>,IOrderDetailRepository 
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
