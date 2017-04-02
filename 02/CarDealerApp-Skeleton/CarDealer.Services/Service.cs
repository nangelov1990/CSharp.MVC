namespace CarDealer.Services
{
    using Data;

    public class Service
    {
        private CarDealerContext context;

        public Service()
        {
            this.context = new CarDealerContext();
        }

        protected CarDealerContext Context => this.context;
    }
}