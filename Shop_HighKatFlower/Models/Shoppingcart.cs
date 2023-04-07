using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Shop_HighKatFlower.Models
{
    public class Shoppingcart
    {
        public List<ShoppingcartItem> Items { get; set; }
        public Shoppingcart() { 
                this.Items =new List<ShoppingcartItem>();
        }
        public void Addtocart(ShoppingcartItem item,int quantity)
        {
            var checkExits = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (checkExits != null)
            {
                checkExits.Quanity += quantity;
                checkExits.PriceTotal = checkExits.Price * checkExits.Quanity;
            }
            else
            {
                Items.Add(item);
            }
        }
        public void Remove(int id)
        {
            var checkExits=Items.SingleOrDefault(x=>x.ProductId == id);
            if(checkExits != null)
            {
                Items.Remove(checkExits);
              
            }
        }
        public void UpdateQuanity(int id, int quantity) {
            var checkExits = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkExits != null)
            {
                checkExits.Quanity=quantity;
                checkExits.PriceTotal=checkExits.Price * checkExits.Quanity;

            }
        }
        public decimal GetTotalPrice()
        {
            return Items.Sum(x => x.PriceTotal);
        }
        public int GetTotalQuanity()
        {
            return Items.Sum(x => x.Quanity);
        }
        public void Clear()
        {
            Items.Clear();
        }
    }
    public class ShoppingcartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }   
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
        public int Quanity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal { get; set; }
    }
}