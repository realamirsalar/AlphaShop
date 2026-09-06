using System.ComponentModel.DataAnnotations;

namespace AlphaShop.Web.ViewModels.AdminUser
{
    public class CreateAdminUserVM
    {
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا شماره تلفن خود را وارد کنید.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا نام خود را وارد کنید.")]
        public string Name { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد کنید.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تأیید رمز عبور")]
        [Required(ErrorMessage = "لطفا تایید رمز عبور خود را وارد کنید.")]
        [Compare("Password", ErrorMessage = "رمز عبور و تأیید رمز عبور مطابقت ندارند.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
