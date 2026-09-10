using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Domain.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public long Price { get; set; }

    }
}
