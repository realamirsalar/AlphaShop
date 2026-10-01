using System.ComponentModel.DataAnnotations;

namespace AlphaShop.Web.ViewModels.Shopping
{
    public class AddressVM
    {
        [Display(Name = "آدری دقیق")]
        [Required(ErrorMessage = "آدرس نمیتواند خالی باشد")]
        public string StreetAddress { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "شهر نمیتواند خالی باشد")]
        public string City { get; set; }
        [Display(Name = "استان")]
        [Required(ErrorMessage = "استان نمیتواند خالی باشد")]
        public string State { get; set; }
        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "کد پستی نمیتواند خالی باشد")]
        public int PostalCode { get; set; }
    }
}
