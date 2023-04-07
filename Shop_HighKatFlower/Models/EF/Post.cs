﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_HighKatFlower.Models.EF
{
    public class Post:CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Vui lòng không để trống tựa bài viết")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        [StringLength(150)]
        public string Image { get; set; }
        public int CategoryId { get; set; }
        [StringLength(150)]
        public string SeoTitle { get; set; }
        [StringLength(550)]
        public string SeoDescription { get; set; }
        [StringLength(100)]
        public string SeoKeywords { get; set; }
        public bool IsActive { get; set; }
        public virtual Post Posts { get; set; }
    }
}