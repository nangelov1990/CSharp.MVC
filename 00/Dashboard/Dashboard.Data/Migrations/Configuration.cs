namespace Dashboard.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<DashboardContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DashboardContext context)
        {
            SeedUserRoles(context);
            SeedAdminUser(context);
        }

        private static void SeedUserRoles(DashboardContext context)
        {
            var notAllUserRolesExist = true;

            while (notAllUserRolesExist)
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                var noAdminRoleExists = !context.Roles.Any(r => r.Name == "Admin");
                var noEmployeeRoleExists = !context.Roles.Any(r => r.Name == "Employee");
                var noCustomerRoleExists = !context.Roles.Any(r => r.Name == "Customer");

                IdentityRole role = null;
                if (noAdminRoleExists)
                {
                    role = new IdentityRole("Admin");
                    noAdminRoleExists = false;
                }
                else if (noEmployeeRoleExists)
                {
                    role = new IdentityRole("Employee");
                    noEmployeeRoleExists = false;
                }
                else if (noCustomerRoleExists)
                {
                    role = new IdentityRole("Customer");
                    noCustomerRoleExists = false;
                }

                if (role != null)
                    manager.Create(role);

                notAllUserRolesExist = noAdminRoleExists ||
                                       noCustomerRoleExists ||
                                       noEmployeeRoleExists;
            }
        }

        private static void SeedAdminUser(DashboardContext context)
        {
            const string email = "app.admin@dashboard.net";
            if (!context.Users.Any(u => u.Email == email))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                const string name = "Application Admin";
                const string password = "Aa!@12";
                var user = new ApplicationUser
                {
                    UserName = email,
                    Name = name,
                    Email = email,
                };

                manager.Create(user, password);
                var createdUser = context.Users.FirstOrDefault(u => u.Email == email);
                if (createdUser != null)
                {
                    manager.AddToRole(createdUser.Id, "Admin");
                    Admin admin = new Admin()
                    {
                        ApplicationUser = user
                    };

                    context.Admins.Add(admin);
                    context.SaveChanges();
                }
            }
        }
    }
}