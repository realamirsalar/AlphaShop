using AlphaShop.Application.Common.DTOs;
using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Common.Utitlity;
using AlphaShop.Application.Services.Implementations;
using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Services.Interfaces
{
    public class OrderService : IOrderService
    {
        private readonly IOrderHeaderRepository _orderHeaderRepo;
        private readonly IOrderDetailRepository _orderDetailRepository;

        private readonly IShoppingCartService _shoppingCartService;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderHeaderRepository orderHeaderRepo, IShoppingCartService shoppingCartService, IUnitOfWork unitOfWork, IOrderDetailRepository orderDetailRepository)
        {
            _orderHeaderRepo = orderHeaderRepo;
            _shoppingCartService = shoppingCartService;
            _unitOfWork = unitOfWork;
            _orderDetailRepository = orderDetailRepository;
        }
        public OrderHeaderDTO CreateOrder(AddressDTO address)
        {
            var order = new OrderHeader()
            {
                State = address.State,
                StreetAddress = address.StreetAddress,
                City = address.City,
                PostalCode = address.PostalCode,
                OrderDate = DateTime.UtcNow,
                UserId = address.UserId,
                OrderStatus = SD.StatusPending,
                PaymentStatus = SD.StatusNotPaied
            };
            int sum = 0;
            var carts = _shoppingCartService.GetAllShoppingCarts(userId: address.UserId);
            var orderDetails = new List<OrderDetail>();
            foreach (var cart in carts)
            {
                orderDetails.Add(new OrderDetail
                {
                    ProductId = cart.ProductId,
                    Count = cart.Count,
                    Price = cart.Product.Price,
                });
                sum += cart.Count * cart.Product.Price;
            }
            order.OrderTotal = sum;
            order.OrderDetails = orderDetails;
            order.UserId = address.UserId;
            _orderHeaderRepo.Add(order);
            _shoppingCartService.DeleteAllShoppingCart(userId: address.UserId);
            _unitOfWork.Save();

            return new OrderHeaderDTO
            {
                Id = order.Id,
                TotalCost = sum,
            };
        }

        public List<OrderListDTO> GetAllOrders()
        {
            return _orderHeaderRepo.GetListOfOrders();
        }

        public OrderHeader GetOrderByAuthority(string authority)
        {
            return _orderHeaderRepo.Get(o => o.Authority == authority);
        }

        public OrderHeader GetOrderById(int orderid)
        {
            var order = _orderHeaderRepo.Get(o => o.Id == orderid,includeProperties: "ApplicationUser");
            if (order == null)
            {
                return null;
            }
            var orderDetails = _orderDetailRepository.GetAll(d => d.OrderHeaderId == orderid, "Product").ToList();
            order.OrderDetails = orderDetails;
            return order;
        }

        public void UpdateOrder(OrderHeader order)
        {
            _orderHeaderRepo.Update(order);
            _unitOfWork.Save();
        }

        public void UpdatePaymentStatus(int orderId, string authority, string paymentGetway, string? refId = null)
        {
            var order = _orderHeaderRepo.Get(o => o.Id == orderId, tracked: true);
            if (order == null)
            {
                return;
            }
            order.Authority = authority;
            order.PaymentGateway = paymentGetway;
            if (refId != null)
            {
                order.RefId = refId;
                order.OrderStatus = SD.StatusApproved;
                order.PaymentStatus = SD.StatusPaied;
                order.PaymentDate = DateTime.UtcNow;
                order.ShoppingDate = DateTime.UtcNow;
            }
            _orderHeaderRepo.Update(order);
            _unitOfWork.Save();
        }
    }
}
