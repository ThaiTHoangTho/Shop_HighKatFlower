using Shop_HighKatFlower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_HighKatFlower.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db=new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(int ? id)
        {
            var item = db.Products.ToList();
            if (id != null)
            {
                item = item.Where(x => x.ProductCategoryId == id).ToList();
            }
            return View(item);
        }
        public ActionResult ProductCategory(string alias,int id)
        {
            var item = db.Products.ToList();
            if (id > 0)
            {
                item = item.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(item);
        }
        public ActionResult Detail(string alias, int id)
        {
            var item = db.Products.Find(id);
            return View(item);
        }
        public ActionResult Partial_ItemsbyCategoryId()
        {
            var items=db.Products.Where(p=>p.IsHome && p.IsActive).Take(12).ToList();
            return PartialView("_Partial_ItemsbyCategoryId",items);
        }
        public ActionResult Partial_ProductsSale()
        {
            var items = db.Products.Where(p => p.IsHome && p.IsHot==true).Take(12).ToList();
            return PartialView("_Partial_ProductsSale", items);
        }
    }
}