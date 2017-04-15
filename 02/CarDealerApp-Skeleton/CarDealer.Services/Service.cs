namespace CarDealer.Services
{
    using System.Linq;
    using Data;

    public class Service
    {
        private CarDealerContext context;

        public Service()
        {
            this.context = new CarDealerContext();
        }

        public string GetLoggedUsername(string sessionId)
        {
            var login = this.Context.Logins
                .FirstOrDefault(l => l.SessionId == sessionId);

            return login?.User.Username;
        }

        protected CarDealerContext Context => this.context;
    }
}