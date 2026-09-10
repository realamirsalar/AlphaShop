using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Infrastructure.Contract.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {

        private readonly ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _db.Update(shoppingCart);
        }
    }
}
