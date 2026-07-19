using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        void CreateCategory(Category category);
    }
}
