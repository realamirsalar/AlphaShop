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
    }
}
