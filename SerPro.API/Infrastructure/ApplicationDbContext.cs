using Microsoft.AspNet.Identity.EntityFramework;
using SerPro.API.Models;
using SerPro.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SerPro.API.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<UserMaster>
    {
        public ApplicationDbContext()
            : base("SerProConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            //modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            //modelBuilder.Entity<IdentityUser>().ToTable("Admins");
            modelBuilder.Entity<UserMaster>().ToTable("UserMaster");
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId }).ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId).ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().HasKey<string>(l => l.UserId).ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<Image> Image { get; set; }
    }
}