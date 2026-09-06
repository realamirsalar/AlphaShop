using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaShop.Application.Common.DTOs
{
    public class CreateAdminUserDto
    {
        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
