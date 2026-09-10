using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Services.Interfaces;
using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _ShopRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartService(IShoppingCartRepository shopRepo, IUnitOfWork unitOfWork)
        {
            _ShopRepo = shopRepo;
            _unitOfWork = unitOfWork;
        }

        public void AddShoppingCart(ShoppingCart shoppingCart)
        {
            var cartFromDb = _ShopRepo.Get(s => s.ProductId == shoppingCart.ProductId && s.ApplicationUserId == shoppingCart.ApplicationUserId);
            if (cartFromDb != null)
            {
                cartFromDb.Count += shoppingCart.Count;
                _ShopRepo.Update(cartFromDb);
            }
            else
            {
                _ShopRepo.Add(shoppingCart);
            }
            _unitOfWork.Save();
        }

        public List<ShoppingCart> GetAllShoppingCarts(string userId)
        {
            var carts = _ShopRepo.GetAll(s => s.ApplicationUserId == userId,includeProperties: "Product");

            return carts.ToList();
        }
    }
}
