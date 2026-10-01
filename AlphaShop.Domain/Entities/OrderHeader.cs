using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaShop.Domain.Entities
{
    public class OrderHeader
    {
        public int Id { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime ShoppingDate { get; set; }
        public long OrderTotal { get; set; }

        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentGateway { get; set; }
        public string Authority { get; set; }
        public string RefId { get; set; }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }

        public string RefPostId { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }


    }

}
