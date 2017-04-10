namespace CarDealerApp.Security
{
    using System.Linq;
    using CarDealer.Data;

    public class AuthenticationManager
    {
        private static CarDealerContext context = new CarDealerContext();

        public static bool IsAuthenticated(string sessionId)
        {
            if (context.Logins.Any(l => l.SessionId == sessionId && l.IsActive))
            {
                return true;
            }

            return false;
        }
    }
}