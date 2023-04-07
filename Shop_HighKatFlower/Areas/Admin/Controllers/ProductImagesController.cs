using Shop_HighKatFlower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_HighKatFlower.Areas.Admin.Controllers
{
    public class ProductImagesController : Controller
    {
        private ApplicationDbContext db=new ApplicationDbContext();
        // GET: Admin/ProductImages
        public ActionResult Index(int id)
        {
            var item=db.ProductImages.Where(x=>x.ProductId==id).ToList();

            return View(item);
        }
    }
}