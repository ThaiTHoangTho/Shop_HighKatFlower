using PagedList;
using Shop_HighKatFlower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_HighKatFlower.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db= new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int ? page)
        {
            var items=db.Orders.OrderByDescending(x=>x.CreateDate).ToList();
            if(items==null)
            {
                page=1;
            }
            var pageIndex = page ?? 1;
            var pageSize = 5;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageIndex;
            return View(items.ToPagedList(pageIndex,pageSize));
        }
        public ActionResult Detail(int id)
        {
            var item=db.Orders.Find(id);
            return View(item);

        }
        public ActionResult Partial_ProductDetail(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderID == id).ToList();
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult UpdateStatus(int id,int status)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = status;
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "Unsuccess", Success = false });
        }
    }
}