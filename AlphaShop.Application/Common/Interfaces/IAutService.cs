using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.Interfaces
{
    public interface IAutService
    {
        Task<bool> RegisterAsync();
    }
}
