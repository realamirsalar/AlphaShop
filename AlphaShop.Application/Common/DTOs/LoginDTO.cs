using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.DTOs
{
    public class LoginDTO 
    {
        public LoginDTO(string phoneNumber, string password, bool rememberMe)
        {
            PhoneNumber = phoneNumber;
            Password = password;
            RememberMe = rememberMe ;
        }

        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
