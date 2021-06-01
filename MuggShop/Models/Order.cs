using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MuggShop.Models;



namespace MuggShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address1 { get; set; }
        
        public string Address2 { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string PostCode { get; set; }
        public string Notes { get; set; }

        public DateTime Date { get; set; }
        public string status { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        //owner of the order
        public virtual IdentityUser User { get; set; }
        public string UserId { get; set; }

    }
}
