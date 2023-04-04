using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop_HighKatFlower.Models.EF
{
    public class Contact:CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập thông tin")]
        [StringLength(150,ErrorMessage ="Không được vượt quá 150 kí tự")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 kí tự")]
        public string Email { get; set; }
        public string Website { get; set; }
        public string Message { get; set; }
        public int IsRead { get; set; }
    }
}