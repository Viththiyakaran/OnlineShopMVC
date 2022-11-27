using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreSara.Data;
using OnlineStoreSara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OnlineStoreSara.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _db;
        public ShopController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int pg= 1)
        {


            var catlist = _db.products.Select(x => x.ProductCategory).Distinct().ToList();
            ViewBag.catlist = catlist;

            var listProduct = _db.products.ToList();
            const int pageSize = 12;

            if(pg<1)
            {
                pg = 1;
            }

            int resCount = listProduct.Count();

            var pager = new Pager(resCount,pg,pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = listProduct.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            var cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");

            ViewBag.cart = cart;

            if (cart == null)
            {
                ViewBag.Count = 0;
            }
            else
            {
                ViewBag.Count = cart.Sum(item => item.Qty);
            }
          

            return View(data);

        }

        [HttpGet]
        public IActionResult CatSearch(string cat,int pg = 1 )
        {

            var listProduct = _db.products.ToList().Where(c=> c.ProductCategory.StartsWith(cat));
            const int pageSize = 12;

            if (pg < 1)
            {
                pg = 1;
            }

            int resCount = listProduct.Count();

            var pager = new Pager(resCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = listProduct.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            var cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");

            ViewBag.cart = cart;

            if (cart == null)
            {
                ViewBag.Count = 0;
            }
            else
            {
                ViewBag.Count = cart.Sum(item => item.Qty);
            }


            return View(data);

        }


        public IActionResult Product(int? id)
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

            var cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");

            ViewBag.cart = cart;

            if (cart == null)
            {
                ViewBag.Count = 0;
            }
            else
            {
                ViewBag.Count = cart.Sum(item => item.Qty);
            }

            return View(listData);
            
        }

        public IActionResult Cart()
        {
            var cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");

            ViewBag.cart = cart;

            ViewBag.total = cart.Sum(item => item.Product.ProductPrice * item.Qty);

            if (cart == null)
            {
                ViewBag.Count = 0;
            }
            else
            {
                ViewBag.Count = cart.Sum(item => item.Qty);
            }

            

            return View();

        }

        public IActionResult Remove(int id)
        {
            List<AddToCardItem> cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");

            int index = isExist(id);

            cart.RemoveAt(index);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Cart");
        }


        public IActionResult AddToCart(int id)
        {
            Product productModel = new Product();

            if (SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart") == null)
            {
                List<AddToCardItem> cart = new List<AddToCardItem>();

                cart.Add
                    (
                        new AddToCardItem
                        {
                            Product = _db.products.Find(id),
                            Qty = 1
                        }
                    );
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<AddToCardItem> cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");

                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Qty++;
                }
                else
                {
                    cart.Add
                    (
                        new AddToCardItem
                        {
                            Product = _db.products.Find(id),
                            Qty = 1
                        }
                    );
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index", "Shop");
        }


        private int isExist(int id)
        {
            List<AddToCardItem> cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductID == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult Home()
        {
            var productList = _db.products.FromSqlRaw("select top 8 * from products order by ProductAddDateAndTime desc").ToList();

            var catlist = _db.products.Select(x => x.ProductCategory).Distinct().ToList();

            ViewBag.catlist = catlist;

            var cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");

            ViewBag.cart = cart;

            if (cart == null)
            {
                ViewBag.Count = 0;
            }
            else
            {
                ViewBag.Count = cart.Sum(item => item.Qty);
            }

            return View(productList);

        }

       
        public IActionResult Checkout(string userEmail)
        {
            if (userEmail == null || userEmail.Equals(""))
            {
                return RedirectToAction("CreateCustomer", "Dashboard");
            }
             

            var listData = _db.users.FirstOrDefault(u => u.userEmail == userEmail);

            ViewBag.usersData = listData;

            if (listData == null)
            {
                return NotFound();
            }
            
           

            var cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");

            ViewBag.cart = cart;

            ViewBag.total = cart.Sum(item => item.Product.ProductPrice * item.Qty);

            if (cart == null)
            {
                ViewBag.Count = 0;
            }
            else
            {
                ViewBag.Count = cart.Sum(item => item.Qty);
            }
            return View(listData);
        }

       
        public IActionResult PlaceOrder(Users users)
        {

            BillHeader billHeader = new BillHeader();

            billHeader.billFirstName = users.userFirstName;
            billHeader.billLastName = users.userLastName;
            billHeader.billEmail = users.userEmail;
            billHeader.billPhone = users.userPhone;
            billHeader.billAddressLine1 = users.userAddressLine1;
            billHeader.billAddressLine2 = users.userAddressLine2;
            billHeader.billTown = users.userTown;
            billHeader.billProvince = users.userProvince;


            var cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");

            ViewBag.cart = cart;

            ViewBag.total = cart.Sum(item => item.Product.ProductPrice * item.Qty);



            _db.billHeader.Add(billHeader);
            _db.SaveChanges();

            List<BillDetail> billDetail = new List<BillDetail>();
            foreach (var item in ViewBag.cart)
            {

                _db.billDetail.Add(new BillDetail
                {
                    
                    billHeaderID = billHeader.billHeaderId,
                    billProduct = item.Product.ProductName,
                    billPrice = item.Product.ProductPrice,
                    billQty = item.Qty,
                    productID = item.Product.ProductID

                });

                string sql = "Update products set ProductStock = ProductStock -" + item.Qty + " Where productID= " + item.Product.ProductID;

                var stocks = _db.Database.ExecuteSqlRaw(sql);

                _db.SaveChanges();
            }

          

            HttpContext.Session.Remove("cart");

            return RedirectToAction("Index");
           // return View("Index");
        }

        public IActionResult UpdateProductQtyPlus(int id)
        {
            List<AddToCardItem> cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");

            int index = isExist(id);

           
            cart[index].Qty++;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Cart");
        }

        public IActionResult UpdateProductQtyMin(int id)
        {
            List<AddToCardItem> cart = SessionHelper.GetObjectFromJson<List<AddToCardItem>>(HttpContext.Session, "cart");

            int index = isExist(id);

            if (cart[index].Qty >= 1) {
                cart[index].Qty--;

            }
            else
            {
                cart[index].Qty = 0;
            }
            

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Cart");
        }

    }

}
