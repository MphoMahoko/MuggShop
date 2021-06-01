using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using MuggShop.Data;

namespace MuggShop.Controllers
{
    public class MessagesController : Controller
    {
        ApplicationDbContext _adb;

        public MessagesController(ApplicationDbContext adb)
        {
            _adb = adb;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            var messages = _adb.Messages.ToList();
            return View(messages);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var message = _adb.Messages.SingleOrDefault(m=>m.Id == id);
            return View(message);
        }

        public IActionResult Add()
        {
            ViewBag.name = "hilili";
            ViewData["Alert"] = "hololo";
            TempData["alert"] = "halala";

            return View();
        }

        [HttpPost]
        public IActionResult Add(Models.Message message)
        {
            if (ModelState.IsValid)
            {
                message.dateTime = DateTime.Now;
                _adb.Messages.Add(message);
                _adb.SaveChanges();
                return Content("Message sent");
            }
            else
            {
                return View();
            }

           
        }
    }
}