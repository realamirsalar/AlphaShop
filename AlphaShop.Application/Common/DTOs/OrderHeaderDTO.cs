using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.DTOs
{
    public class OrderHeaderDTO
    {
        public int Id { get; set; }
        public int TotalCost { get; set; }
        public string CallBackUrl { get; set; }
    }
}
