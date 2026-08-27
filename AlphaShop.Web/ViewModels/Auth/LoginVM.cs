using System.ComponentModel.DataAnnotations;

namespace AlphaShop.Web.ViewModels.Auth
{
    public class LoginVM
    {

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا شماره تلفن خود را وارد کنید.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد کنید.")]
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپار")]
        public bool RemeberMe { get; set; }

    }
}
