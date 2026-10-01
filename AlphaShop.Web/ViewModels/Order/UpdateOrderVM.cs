using AlphaShop.Application.Common.Utitlity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AlphaShop.Web.ViewModels.Order
{
    public class UpdateOrderVM
    {
        public UpdateOrderVM()
        {
            
        }
        public UpdateOrderVM(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public string Status { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; } 
        [Required(ErrorMessage = "این فیلد الزامی میباشد!")]
        public string RefPostId { get; set; }
    }
}
