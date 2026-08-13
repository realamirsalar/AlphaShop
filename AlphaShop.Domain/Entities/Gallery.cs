using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaShop.Domain.Entities
{
    public class Gallery
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
