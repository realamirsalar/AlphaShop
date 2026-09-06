using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaShop.Web.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "مقداری برای {0} وارد کنید.")]
        [MaxLength(20, ErrorMessage = "این بخش نمیتواند بیشتر از {1} باشد.")]
        public string Name { get; set; }

        [Display(Name = "عکس")]
        public IFormFile Image { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "مقداری برای {0} وارد کنید.")]
        [MaxLength(300, ErrorMessage = "این بخش نمیتواند بیشتر از {1} باشد.")]
        public string Description { get; set; }

        [Display(Name = "تگ ها")]
        [MaxLength(50, ErrorMessage = "این بخش نمیتواند بیشتر از {1} باشد.")]
        public string Tags { get; set; }

        [Display(Name = "قیمت قدیمی")] public int? OldPrice { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "مقداری برای {0} وارد کنید.")]
        public int Price { get; set; }

        [Display(Name = "موجودی")]
        [Required(ErrorMessage = "مقداری برای {0} وارد کنید.")]
        public int Quntity { get; set; }
        
        [Display(Name = "نمایش ندادن")] public bool NotShow { get; set; }

        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories{ get; set; }
    }
}
