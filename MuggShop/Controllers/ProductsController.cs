using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuggShop.Data;
using MuggShop.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MuggShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.Single(p => p.Id == id);
            return View(product);
        }

        [HttpPost]
        //public IActionResult Add(Product product, IFormFile PictureURL)
        public IActionResult Edit(Product editProduct, IFormFile PictureURL)
        {
            if (!ModelState.IsValid)
            {
                var id = editProduct.Id;
                return View(editProduct);
            }
            else
            {
                var picname = DateTime.Now.ToString("ddMMyyyHHmmss") + PictureURL.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploadImages", picname);

                var product = _context.Products.Single(p => p.Id == editProduct.Id);

                product.Name = editProduct.Name;
                product.Description = editProduct.Description;
                product.Price = editProduct.Price;
                product.PictureURL = picname;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    PictureURL.CopyToAsync(stream);
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public IActionResult Index(string alert = "")
        {
            if (alert != "")
            {
                ViewBag.name = "hilili";
                ViewData["Alert"] = "hololo";
                TempData["alert"] = "halala";
            }

            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Single(int id, string alert = "")
        {
            if (alert != "")
            {
                ViewBag.name = "hilili";
                ViewData["Alert"] = "hololo";
                TempData["alert"] = "halala";
            }
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            return View(product);
        }


        [HttpPost]
        public IActionResult Add(Product product, IFormFile PictureURL)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var picname = DateTime.Now.ToString("ddMMyyyHHmmss") + PictureURL.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploadImages", picname);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    PictureURL.CopyToAsync(stream);
                }

                var pro = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    PictureURL = picname
                };

                _context.Products.Add(pro);
                _context.SaveChanges();

                var products = _context.Products.ToList().Take(3);

                

                return View("Index", products);
            }
                
            
        }











      







    }
}