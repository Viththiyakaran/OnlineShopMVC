using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreSara.Data;
using OnlineStoreSara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreSara.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;

        public DashboardController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(Admin admin)
        {
            if (admin.username == "adm@gmail.com" && admin.password == "1234")
            {
                HttpContext.Session.SetString("userId", admin.username);
                HttpContext.Session.SetString("userType", "Admin");
                TempData["Login"] = "Login Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Input Errors";
                return View(admin);
            }
        }

        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomer(Users users)
        {
            if (ModelState.IsValid)
            {
                if(users.userPassword == users.userRePassword)
                {
                    _db.users.Add(users);
                    _db.SaveChanges();
                    TempData["Create"] = "Created New Successfully";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Error"] = "Please check the inputs";
                }
                
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(Users users)
        {
           
                var data = _db.users.Where(u => u.userEmail == users.userEmail).SingleOrDefault();
                if( data != null)
                {
                    bool isValid = (data.userEmail == users.userEmail && data.userPassword == users.userPassword);
                    if( isValid )
                    {
                        HttpContext.Session.SetString("userId", users.userEmail);
                        HttpContext.Session.SetString("userType", "Customer");
                        TempData["Login"] = "Login Successfully";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["Error"] = "Input Errors";
                        return View(users);
                    }
                }
                else
                {
                    TempData["Error"] = "Input Errors";
                    return View(users);
                }
           
           
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

    }
}
