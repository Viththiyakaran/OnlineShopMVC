using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStoreSara.Data;
using OnlineStoreSara.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreSara.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(AppDbContext db,IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable <Product> listData = _db.products.ToList();

            return View(listData);
        }
        public IActionResult Create()
        {
            ViewData["ManufacturerID"] = new SelectList(_db.manufacturers, "ManufacturerID", "ManufacturerName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product Obj)
        {
            
                
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(Obj.ProductImageFile.FileName);
                string extension = Path.GetExtension(Obj.ProductImageFile.FileName);
                Obj.ProductImageName= fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    Obj.ProductImageFile.CopyTo(fileStream);
                }

                _db.products.Add(Obj);
                _db.SaveChanges();
                TempData["Create"] = "Created New Successfully";
                ViewData["ManufacturerID"] = new SelectList(_db.manufacturers, "ManufacturerID", "ManufacturerName");
                return RedirectToAction("Index");
           

          
        }

        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var listData = _db.products.Find(id);

            if (listData == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerID"] = new SelectList(_db.manufacturers, "ManufacturerID", "ManufacturerName");
            return View(listData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product Obj)
        {
           

                if (Obj.ProductImageFile != null)
                {
                    if (Obj.ProductImageName != null)
                    {

                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", Obj.ProductImageName);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                }

                Obj.ProductImageName = ProcessUploadedFile(Obj);

                _db.products.Update(Obj);
                _db.SaveChanges();
                TempData["Update"] = "Edit Item Successfully Done";
                ViewData["ManufacturerID"] = new SelectList(_db.manufacturers, "ManufacturerID", "ManufacturerName");
                return RedirectToAction("Index");
            

         

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var listData = _db.products.Find(id);

            if (listData == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerID"] = new SelectList(_db.manufacturers, "ManufacturerID", "ManufacturerName");
            return View(listData);

        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var listData = _db.products.Find(id);

            if (listData == null)
            {
                return NotFound();
            }

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", listData.ProductImageName);
            if (System.IO.File.Exists(imagePath))
            { 
                System.IO.File.Delete(imagePath); 
            }
                

            _db.products.Remove(listData);
            _db.SaveChanges();
            TempData["Delete"] = "Delete has been done";
            ViewData["ManufacturerID"] = new SelectList(_db.manufacturers, "ManufacturerID", "ManufacturerName");
            return RedirectToAction("Index");



        }

        private string ProcessUploadedFile(Product Obj)
        {
            string fileName = null;
            

            if (Obj.ProductImageFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                fileName = Path.GetFileNameWithoutExtension(Obj.ProductImageFile.FileName);
                string extension = Path.GetExtension(Obj.ProductImageFile.FileName);
                Obj.ProductImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    Obj.ProductImageFile.CopyTo(fileStream);
                }
            }

            return fileName;
        }

        public IActionResult Order()
        {
          
            var orders = _db.billHeader.Where(item => item.isOrderPlaced ==false).ToList() ;

            return View(orders);
        }
        
       
        public IActionResult ViewOrder(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var listData = _db.billDetail.ToList().Where(item => item.billHeaderID == id);

            if (listData == null)
            {
                return NotFound();
            }
          
            return View(listData);
        }

    }
}

