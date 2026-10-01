using AlphaShop.Application.Common.DTOs;
using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Common.Utitlity;
using AlphaShop.Domain.Entities;
using AlphaShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop.Infrastructure.Contract.Repositories
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {

        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public List<OrderListDTO> GetListOfOrders()
        {
            var ordersDTO = _db.OrderHeaders.Select(o => new OrderListDTO
            {
                Count = o.OrderDetails.Count(),
                Date = o.OrderDate.ToIranPersianDateString(),
                Id = o.Id,
                Name = o.ApplicationUser.Name,
                Status = o.OrderStatus,
                PhoneNumber = o.ApplicationUser.PhoneNumber,
                TotalCost = (int)o.OrderTotal
            }).ToList();
            return ordersDTO;

        }

        public OrderHeader GetOrderById(int id)
        {
            return _db.OrderHeaders.Where(o => o.Id == id)
                .Include(o => o.ApplicationUser)
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product).FirstOrDefault();
        }

        public void Update(OrderHeader orderHeader)
        {
            _db.OrderHeaders.Update(orderHeader);
        }

    }
}
