namespace SerPro.API.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SerPro.API.Infrastructure;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SerPro.API.Infrastructure.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SerPro.API.Infrastructure.ApplicationDbContext context)
        {
            var manager = new UserManager<UserMaster>(new UserStore<UserMaster>(new ApplicationDbContext()));

            var user = new UserMaster()
            {
                UserName = "Pratik",
                Email = "p.kankhara@gmail.com",
                EmailConfirmed = true,
                FirstName = "Pratik",
                LastName = "Kankhara",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "Abcd1@3");
        }
    }
}
