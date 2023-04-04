using Microsoft.Ajax.Utilities;
using Shop_HighKatFlower.Models;
using Shop_HighKatFlower.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_HighKatFlower.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db=new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var item = db.Categories;
            return View(item);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category category)
        {
            if(ModelState.IsValid)
            {
                category.CreateDate = DateTime.Now;
                category.ModifiedDate = DateTime.Now;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var item=db.Categories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(category);
                category.ModifiedDate = DateTime.Now;
                db.Entry(category).Property(x => x.Title).IsModified = true;
                db.Entry(category).Property(x => x.Description).IsModified = true;
                db.Entry(category).Property(x => x.SeoDescription).IsModified = true;
                db.Entry(category).Property(x => x.SeoKeywords).IsModified = true;
                db.Entry(category).Property(x => x.SeoTitle).IsModified = true;
                db.Entry(category).Property(x => x.Position).IsModified = true;
                db.Entry(category).Property(x => x.ModifiedDate).IsModified = true;
                db.Entry(category).Property(x => x.Modifiedby).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Categories.Find(id);
            if(item != null)
            {
                db.Categories.Remove(item);
                db.SaveChanges();
                return Json(new { success=true });
            }
            return Json(new { success = false });
        }
    }
}