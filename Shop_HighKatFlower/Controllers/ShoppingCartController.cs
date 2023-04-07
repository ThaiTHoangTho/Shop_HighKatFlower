using Microsoft.Ajax.Utilities;
using Shop_HighKatFlower.Models;
using Shop_HighKatFlower.Models.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_HighKatFlower.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {

            Shoppingcart cart = (Shoppingcart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOutSuccess()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            Shoppingcart cart = (Shoppingcart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(OrderViewModel orderView)
        {
            var code = new { Success = false,code = -1 };
            if (ModelState.IsValid)
            {
                Shoppingcart cart = (Shoppingcart)Session["Cart"];
                if (cart != null)
                {
                   Order order = new Order();
                    order.CustomerName = orderView.CustomerName;
                    order.Phone = orderView.Phone;
                    order.Address= orderView.Address;
                    order.Email = orderView.Email;
                    cart.Items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        ProductID = x.ProductId,
                        Quanity=x.Quanity,
                        Price=x.Price
                    }));
                    order.TotalAmount = cart.Items.Sum(x => (x.Price * x.Quanity));
                    order.TypePayment = orderView.TypePayment;
                    order.CreateDate=DateTime.Now;
                    order.ModifiedDate=DateTime.Now;
                    order.CreateBy = orderView.Phone;
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.Orders.Add(order);
                    db.SaveChanges();
                    //send mail
                    var strSanPham = "";
                    var thanhtien = decimal.Zero;
                    var TongTien = decimal.Zero;
                    foreach (var sp in cart.Items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>" + sp.ProductName + "</td>";
                        strSanPham += "<td>" + sp.Quanity + "</td>";
                        strSanPham += "<td>" + Shop_HighKatFlower.Common.Common.FormatNumber(sp.PriceTotal, 0) + "</td>";
                        strSanPham += "</tr>";
                        thanhtien += sp.Price * sp.Quanity;
                    }
                    TongTien = thanhtien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/send/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}", order.Code);
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", orderView.Email);
                    contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", Shop_HighKatFlower.Common.Common.FormatNumber(thanhtien, 0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", Shop_HighKatFlower.Common.Common.FormatNumber(TongTien, 0));
                    Shop_HighKatFlower.Common.Common.SendMail("HighKat_FLower", "Đơn đặt hàng #" + order.Code, contentCustomer.ToString(), orderView.Email);

                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/send/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{MaDon}}", order.Code);
                    contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                    contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
                    contentAdmin = contentAdmin.Replace("{{Email}}", orderView.Email);
                    contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", Shop_HighKatFlower.Common.Common.FormatNumber(thanhtien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", Shop_HighKatFlower.Common.Common.FormatNumber(TongTien, 0));
                    Shop_HighKatFlower.Common.Common.SendMail("HighKat_FLower", "Đơn hàng mới #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);


                    cart.Clear();
                    return RedirectToAction("CheckOutSuccess");
                }
            }    
            return Json(code);
        }
        public ActionResult Partial_Item_Pay()
        {
            Shoppingcart cart = (Shoppingcart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult Partial_Item_Cart()
        {
            Shoppingcart cart = (Shoppingcart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult ShowCount()
        {
            Shoppingcart cart = (Shoppingcart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }
      
        [HttpPost]
        public ActionResult Addtocart(int id,int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var checkProduct=db.Products.FirstOrDefault(x=>x.Id == id);
            if (checkProduct != null)
            {
                Shoppingcart cart = (Shoppingcart)Session["Cart"];
                if (cart == null)
                {
                    cart = new Shoppingcart();
                }
                ShoppingcartItem shoppingcart = new ShoppingcartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Alias=checkProduct.Alias,
                    Quanity = quantity


                };
                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    shoppingcart.ProductImg = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                }
                shoppingcart.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    shoppingcart.Price = (decimal)checkProduct.PriceSale;
                }
                shoppingcart.PriceTotal = shoppingcart.Quanity * shoppingcart.Price;
                cart.Addtocart(shoppingcart, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm vào giỏ hàng thành công", code = 1, Count = cart.Items.Count };
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            Shoppingcart cart = (Shoppingcart)Session["Cart"];
            if (cart != null)
            {
                var checkPro=cart.Items.FirstOrDefault(x=>x.ProductId == id);
                if (checkPro != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.Items.Count };

                }
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult DeleteAll()
        {
            Shoppingcart cart = (Shoppingcart)Session["Cart"];
            if (cart!=null)
            {
                cart.Clear();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            Shoppingcart cart = (Shoppingcart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuanity(id,quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
    }
}