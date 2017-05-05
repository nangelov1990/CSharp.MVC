namespace Dashboard.Models.EntityModels
{
    public abstract class DashboardUser
    {
        public int Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
