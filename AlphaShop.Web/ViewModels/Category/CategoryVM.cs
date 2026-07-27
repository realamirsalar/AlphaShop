using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AlphaShop.Web.ViewModels.Category
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
        [MaxLength(100 , ErrorMessage ="این فیلد باید کمتر از 100 کارکترباشد")]
        public string Name { get; set; }
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        [Range(0,100 , ErrorMessage ="لطفا عددی مابین 0 تا 100 وارد کنید")]
        public int DisplayOrder { get; set; }
        public int? ParentId { get; set; }
        public IEnumerable<SelectListItem>? ParentLists { get; set; }
    }
}
