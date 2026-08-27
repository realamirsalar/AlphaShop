

namespace AlphaShop.Application.Common.DTOs
{
    public class RegisterDTO
    {
        public RegisterDTO(string name, string phoneNumber, string password)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Password = password;
        }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
