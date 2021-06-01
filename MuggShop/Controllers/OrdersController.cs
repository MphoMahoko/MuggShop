using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuggShop.Data;
using MuggShop.Models;
using MuggShop.ViewModels;

namespace MuggShop.Controllers
{
   
    public class OrdersController : Controller
    {
        ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(string message = "")
        {
            if (message != "")
            {
                TempData["status"] = message;
                
            }

            var orders = _context.Orders.Include(o=>o.OrderDetails).ToList();

            return View(orders);
        }

        public IActionResult MyOrders(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            

            var orders = _context.Orders.Where(o => o.UserId == userId).ToList();
            //return View(orders);
            return View(orders);
        }

        [Authorize]
        public IActionResult Add()
        {
            var order = new Order();
            var cartId = GetCartId();

            var cartItems = _context.CartItems.Where(c => c.CartId == cartId).Include(c => c.Product).ToList();


            var shoppingcartOrder = new ShoppingCartOrder {
            CartItems = cartItems,
            Order = order
            };

            return View(shoppingcartOrder);
        }




        public IActionResult Process(int id, string status)
        {
            var order = _context.Orders.SingleOrDefault(o => o.Id == id);
            order.status = status;
            _context.SaveChanges();

            TempData["status"] = "sex";
            ViewData["status"] = "view data sex";
            ViewBag.status = "view bag sex";


            return RedirectToAction("Index", new {message = status });

            //return Redirect(Request.Headers["Referer"].ToString());

            // return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Details(int id)
        {

            // var order = _context.Orders.Include(o => o.OrderDetails).SingleOrDefault(o => o.Id == id);
            var order = _context.Orders.Include("OrderDetails.Product").SingleOrDefault(o => o.Id == id);

            //var orderdetail = _context.OrderDetails.Include(od => od.Product).Include(c => c.Order).Where(a => a.OrderId == id).FirstOrDefault();




            return View(order);
        }


        [HttpPost]
        public IActionResult Add(Order order)
        {
            if (!ModelState.IsValid)
            {
              
                return View(order);
            }
            else 
            {
              

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var cartId = GetCartId();
                var cartItems = _context.CartItems.Include(c => c.Product).Where(c => c.CartId == cartId).ToList();

                var ord = new Order
                {
                    Address1 = order.Address1,
                    Address2 = order.Address2,
                    Email = order.Email,
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    Notes = order.Notes,
                    Phone = order.Phone,
                    PostCode = order.PostCode,
                    Province = order.Province,
                    status = "pending",
                    Date = DateTime.Now,
                    UserId = userId
                };

                foreach (var item in cartItems)
                {
                    var orderdetail =
                     new OrderDetails
                     {
                         Count = item.Count,
                         ProductId = item.ProductId,
                         Total = item.Product.Price * item.Count,
                         OrderId = ord.Id
                     };

                    ord.OrderDetails.Add(orderdetail);
                    _context.CartItems.Remove(item);
                }

                _context.Orders.Add(ord);


                _context.SaveChanges();

                return RedirectToAction("MyOrders", new { id = userId });
            }

            
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