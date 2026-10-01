using AlphaShop.Application.Common.DTOs;
using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.Interfaces
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        List<OrderListDTO> GetListOfOrders();
        void Update(OrderHeader orderHeader);
        OrderHeader GetOrderById(int id);

    }
}
