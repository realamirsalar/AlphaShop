using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaShop.Application.Common.DTOs
{
    public class PaymentResponseDTO
    {
        [Required]
        public bool IsSuccess { get; set; }
        public string PaymentUrl { get; set; }
        public string Authority { get; set; }
        public string ErrorMessage { get; set; }
        public string InfoMessage { get; set; }
    }
}
