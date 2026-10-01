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

        public void DeleteAllShoppingCart(string userId)
        {
            var carts = _ShopRepo.GetAll(c => c.ApplicationUserId == userId).ToList();
            if(carts.Count > 0)
            {
                _ShopRepo.DeleteRange(carts);
            }
        }

        public List<ShoppingCart> GetAllShoppingCarts(string userId)
        {
            var carts = _ShopRepo.GetAll(s => s.ApplicationUserId == userId,includeProperties: "Product");

            return carts.ToList();
        }

        public ShoppingCart GetById(int id)
        {
            return _ShopRepo.Get(s => s.Id == id, includeProperties: "Product");
        }

        public bool Minus(int id)
        {
            var cart = GetById(id);
            if (cart != null && cart.Count > 0)
            {
                cart.Count--;
                if(cart.Count == 0)
                {
                    _ShopRepo.Remove(cart);
                }
                else
                {
                _ShopRepo.Update(cart);
                }
                _unitOfWork.Save();
                return true;

            }
            return false;
        }

        public bool Plus(int id)
        {
            var cart = GetById(id);
            if (cart != null && cart.Count < cart.Product.Quntity)
            {
                cart.Count++;
                _ShopRepo.Update(cart);
                _unitOfWork.Save();
                return true;

            }
            return false;
        }

        public bool RemoveShoppingCart(int cartId)
        {
            var cart = _ShopRepo.Get(c => c.Id == cartId);
            if (cart == null)
            {
                return false;
            }
            _ShopRepo.Remove(cart);
            _unitOfWork.Save();
            return true;
        }
    }
}
