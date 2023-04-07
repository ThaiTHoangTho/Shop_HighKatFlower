using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Shop_HighKatFlower.Models.EF
{
    public class Order:CommonAbstract
    {
        public Order() {
            this.OrderDetails=new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên khách hàng")]
        public string  CustomerName { get; set; }
        [Phone]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int TypePayment { get; set; }
        public  virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}