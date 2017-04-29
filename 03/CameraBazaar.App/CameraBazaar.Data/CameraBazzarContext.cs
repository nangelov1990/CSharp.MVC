namespace CameraBazaar.Data
{
    using System.Data.Entity;
    using Models.EntityModels;

    public class CameraBazzarContext : DbContext
    {
        public CameraBazzarContext()
            : base("CameraBazzarContext") { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Camera> Cameras { get; set; }
    }
}