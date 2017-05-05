namespace CameraBazaar.Services
{
    using Data;

    public class Service
    {
        private CameraBazzarContext context;

        public Service()
        {
            this.context = new CameraBazzarContext();
        }

        protected CameraBazzarContext Context => this.context;
    }
}
