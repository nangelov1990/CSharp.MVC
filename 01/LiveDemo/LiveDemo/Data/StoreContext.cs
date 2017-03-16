namespace LiveDemo.Data
{
    using System.Data.Entity;

    using Models;

    public class StoreContext : DbContext
    {
        public StoreContext()
            : base("StoreContext") { }

        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }
    }
}