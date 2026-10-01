using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.DTOs
{
    public class OrderListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Date { get; set; }
        public int TotalCost { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
    }
}
