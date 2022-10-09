using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStoreSara.Data;
using OnlineStoreSara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreSara.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _db;

        public UserController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var userList = _db.users.Include("Manufacturer").ToList();
            return View(userList);
        }

        //public IActionResult Create()
        //{

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Users Obj)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _db.users.Add(Obj);
        //        _db.SaveChanges();
        //        TempData["Create"] = "Created New Successfully";
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}

    }
}
