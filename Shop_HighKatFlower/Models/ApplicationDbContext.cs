using Microsoft.AspNet.Identity.EntityFramework;
using Shop_HighKatFlower.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shop_HighKatFlower.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Adv> Advs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Subscribe> subscribes { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}