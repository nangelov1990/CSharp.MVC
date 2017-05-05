namespace Dashboard.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;

    public class DashboardContext : IdentityDbContext<ApplicationUser>
    {
        public DashboardContext()
            : base("data source=.;initial catalog=DashboardSystem;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework",
                  throwIfV1Schema: false)
        {
        }

        public static DashboardContext Create()
        {
            return new DashboardContext();
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Request> Requests { get; set; }

        public System.Data.Entity.DbSet<Dashboard.Models.ViewModels.Request.SingleRequestListView> SingleRequestListViews { get; set; }
    }
}