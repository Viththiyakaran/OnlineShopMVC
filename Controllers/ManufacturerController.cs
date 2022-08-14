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
                TempData["Create"] = "Created New Successfully";
                return RedirectToAction("Index");
            }

            return View();
        }


        public IActionResult Edit(int? id)
        {
            if( id == null || id == 0)
            {
                return NotFound();
            }

            var listData = _db.manufacturers.Find(id);

            if(listData == null)
            {
                return NotFound();
            }

            return View(listData);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Manufacturer Obj)
        {
           if(ModelState.IsValid)
            {
                _db.manufacturers.Update(Obj);
                _db.SaveChanges();
                TempData["Update"] = "Edit Item Successfully Done";
                return RedirectToAction("Index");
            }

            return View();

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var listData = _db.manufacturers.Find(id);

            if (listData == null)
            {
                return NotFound();
            }

            return View(listData);

        }



        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            var listData = _db.manufacturers.Find(id);

            if (listData == null)
            {
                return NotFound();
            }


            _db.manufacturers.Remove(listData);
            _db.SaveChanges();
            TempData["Delete"] = "Delete has been done";
            return RedirectToAction("Index");
        }


    }
}
