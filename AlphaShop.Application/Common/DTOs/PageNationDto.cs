using AlphaShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.DTOs
{
    public class PageNationDto
    {
        public List<Product> Products { get; set; }
        public int PageCount { get; set; }
    }
}
