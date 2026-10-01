using AlphaShop.Application.Common.DTOs;
using AlphaShop.Domain.Entities;


namespace AlphaShop.Application.Services.Implementations
{
    public interface IOrderService
    {
        OrderHeaderDTO CreateOrder(AddressDTO address);

        void UpdatePaymentStatus(int orderid, string authority, string paymentGetway, string? refId = null);

        OrderHeader GetOrderByAuthority(string authority);

        List<OrderListDTO> GetAllOrders();
        OrderHeader GetOrderById(int orderid);
        void UpdateOrder(OrderHeader order);

    }
}
