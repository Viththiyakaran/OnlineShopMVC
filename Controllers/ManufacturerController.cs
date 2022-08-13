using Microsoft.AspNetCore.Mvc;
using OnlineStoreSara.Data;
using OnlineStoreSara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreSara.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly AppDbContext _db;

        public ManufacturerController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Manufacturer> listData = _db.manufacturers.ToList();
            return View(listData);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Manufacturer Obj)
        {

            if(ModelState.IsValid)
            {
                _db.manufacturers.Add(Obj);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }


        public IActionResult Edit(int? Mid)
        {
            return View();

        }
    }
}
