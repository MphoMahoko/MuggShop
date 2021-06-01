using MuggShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuggShop.ViewModels
{
    public class ShoppingCartOrder
    {
        public Order Order { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
    }
}
