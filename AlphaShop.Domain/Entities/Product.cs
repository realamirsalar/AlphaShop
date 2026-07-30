using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaShop.Domain.Entities
{
    public class Product
    {
        [Key] public int Id { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "مقداری برای {0} وارد کنید.")]
        [MaxLength(20, ErrorMessage = "این بخش نمیتواند بیشتر از {1} باشد.")]
        public string Name { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "مقداری برای {0} وارد کنید.")]
        [MaxLength(40, ErrorMessage = "این بخش نمیتواند بیشتر از {1} باشد.")]
        public string ImageUrl { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "مقداری برای {0} وارد کنید.")]
        [MaxLength(300, ErrorMessage = "این بخش نمیتواند بیشتر از {1} باشد.")]
        public string Description { get; set; }

        [Display(Name = "تگ ها")]
        [MaxLength(50, ErrorMessage = "این بخش نمیتواند بیشتر از {1} باشد.")]
        public string Tags { get; set; }

        [Display(Name = "بازدید")] public int Seen { get; set; }
        [Display(Name = "قیمت قدیمی")] public int? OldPrice { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "مقداری برای {0} وارد کنید.")]
        public int Price { get; set; }

        [Display(Name = "موجودی")]
        [Required(ErrorMessage = "مقداری برای {0} وارد کنید.")]
        public int Quntity { get; set; }

        public DateTime RelaseDate { get; set; }
        [Display(Name = "نمایش ندادن")] public bool NotShow { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")] public Category Category { get; set; }
    }

}
