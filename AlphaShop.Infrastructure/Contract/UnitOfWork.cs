using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Infrastructure.Contract
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
