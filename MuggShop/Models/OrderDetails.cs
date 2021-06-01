using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuggShop.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Count { get; set; }
        public decimal Total { get; set; }



    
    }
}
