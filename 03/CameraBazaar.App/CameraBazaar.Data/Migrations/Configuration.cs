namespace CameraBazaar.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Data;

    internal sealed class Configuration : DbMigrationsConfiguration<CameraBazzarContext>
    {
        public Configuration() { AutomaticMigrationsEnabled = false; }

        protected override void Seed(CameraBazzarContext context) { }
    }
}
