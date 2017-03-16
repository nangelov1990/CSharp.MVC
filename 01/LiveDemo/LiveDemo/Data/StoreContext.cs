namespace LiveDemo.Data
{
    using System.Data.Entity;

    public class StoreContext : DbContext
    {
        public StoreContext()
            : base("StoreContext") { }
    }
}