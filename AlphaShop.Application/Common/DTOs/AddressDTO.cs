using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.DTOs
{
    public class AddressDTO
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string UserId { get; set; }
    }
}
