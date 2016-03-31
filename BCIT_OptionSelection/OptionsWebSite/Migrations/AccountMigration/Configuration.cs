namespace OptionsWebSite.Migrations.AccountMigration
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OptionsWebSite.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\AccountMigration";
        }

        protected override void Seed(OptionsWebSite.Models.ApplicationDbContext context)
        {
            //Initialize the managers/stores
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // If role does not exists, create it
            if (!roleManager.RoleExists("Admin")) {
                roleManager.Create(new IdentityRole("Admin"));
            }
            if (!roleManager.RoleExists("Student"))
            {
                roleManager.Create(new IdentityRole("Student"));
            }

            // Create test users
            //Create administrator test user
            var adminUser = userManager.FindByName("A00111111");
            if (adminUser == null)
            {
                var newAdminUser = new ApplicationUser()
                {
                    UserName = "A00111111",
                    Email = "a@a.a"
                };
                var result = userManager.Create(newAdminUser, "P@$$w0rd");
                if (result.Succeeded)
                {
                    userManager.SetLockoutEnabled(newAdminUser.Id, false);
                    userManager.AddToRole(newAdminUser.Id, "Admin");
                }
                
            }
            //Create student test user
            var studentUser = userManager.FindByName("A00222222");
            if (studentUser == null)
            {
                var newStudentUser = new ApplicationUser()
                {
                    UserName = "A00222222",
                    Email = "s@s.s"
                };
                var result = userManager.Create(newStudentUser, "P@$$w0rd");
                if (result.Succeeded)
                {
                    userManager.SetLockoutEnabled(newStudentUser.Id, false);
                    userManager.AddToRole(newStudentUser.Id, "Student");
                }
            }
        }
    }
}
