using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuggShop.Data;
using MuggShop.Models;

namespace MuggShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string alert = "")
        {
                
            if(alert == null)
            {
                return Content("sebenzing");
            }

            var cartId = GetCartId();
          
            var cartItems = _context.CartItems.Where(c=>c.CartId == cartId).Include(c=>c.Product).ToList();
            return View(cartItems);
        }

        public IActionResult MinusCount(int id)
        {
            var cartItem = _context.CartItems.SingleOrDefault(c => c.Id == id);

            if (cartItem.Count > 1)
            {
                cartItem.Count--;
            }
            else
            {
                _context.CartItems.Remove(cartItem);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AddCount(int id)
        {
            var cartItem = _context.CartItems.SingleOrDefault(c => c.Id == id);

           
            cartItem.Count++;
        

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult DeleteItem(int id)
        {
            var cartItem = _context.CartItems.SingleOrDefault(c => c.Id == id);
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult AddToCart(int id)
        {
            ViewData["Alert"] = "Order succesfully placed";

            var product = _context.Products.Single(p => p.Id == id);

            var cartItem = _context.CartItems.SingleOrDefault(c => c.ProductId == product.Id && c.CartId == GetCartId());

            if (cartItem == null)
            {
                cartItem = new CartItem {
                    CartId = GetCartId(), 
                    Product = product, 
                    Count = 1
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }

            _context.SaveChanges();



            


            string url = Request.Headers["Referer"].ToString();

            if (url.Contains("/Products/Single"))
            {
                return RedirectToAction("Single", "Products", new {Id = id, alert = "hololo" });
                
            }
            else if (url.Contains("/Products"))
            {
             
                return RedirectToAction("Index","Products", new { alert = "hololo" });
            }



            return RedirectToAction("Index", new {alert = "hololo"});


             
            // return Redirect(Request.Headers["Referer"].ToString());
            //return Content("Added to cart");
        }

     

        public string GetCartId()
        {
            var cartId = String.Empty;
            var cookie = Request.Cookies["shopcart"];
          

            if (cookie == null)
            {
                //cookie = new CookieOptions().ToString();
                cartId = Guid.NewGuid().ToString();

                var options = new CookieOptions
                {
                    IsEssential = true
                };

                Response.Cookies.Append("shopcart", cartId, options);
            }
            else
            {
                cartId = Request.Cookies["shopcart"];
               
            }

            return cartId;
            
        }

   
    }
}