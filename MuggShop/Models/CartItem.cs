using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuggShop.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string CartId { get; set; }
        public int Count { get; set; }
        public Product Product { get; set; }
    }
}
