using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.DTOs
{
    public class PaymentVerifyResultDto
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public string RefId { get; set; }
        //public int? OrderId { get; set; }
    }
}
