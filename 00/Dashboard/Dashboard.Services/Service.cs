namespace Dashboard.Services
{
    using Data;

    public abstract class Service
    {
        private DashboardContext context;

        public Service()
        {
            this.context = new DashboardContext();
        }

        public DashboardContext Context => this.context;
    }
}
