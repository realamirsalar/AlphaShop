using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Services.Interfaces
{
    public interface IShoppingCartService
    {
        List<ShoppingCart> GetAllShoppingCarts(string userId);

        void AddShoppingCart(ShoppingCart shoppingCart);
        bool RemoveShoppingCart(int cartId);
        ShoppingCart GetById(int id);
        bool Plus(int id);
        bool Minus(int id);
        void DeleteAllShoppingCart(string userId);
    }
}
