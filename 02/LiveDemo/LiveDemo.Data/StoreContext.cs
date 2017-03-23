namespace LiveDemo.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class StoreContext : DbContext
    {
        public StoreContext()
            : base("StoreContext") { }

        public DbSet<Mouse> Mice;
    }
}