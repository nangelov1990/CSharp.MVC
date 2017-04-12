namespace CarDealerApp.Security
{
    using System.Linq;

    using CarDealer.Data;
    using CarDealer.Models.EntityModels;

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

        public static void Logout(string sessionId)
        {
            Login login = context.Logins
                .FirstOrDefault(l => l.SessionId == sessionId);
            login.IsActive = false;
            context.SaveChanges();
        }
    }
}