namespace CarDealer.Services
{
    using System.Linq;
    using AutoMapper;

    using Models.BindingModels.Users;
    using Models.EntityModels;

    public class UserService : Service
    {
        public void AddUser(RegUserBm bind)
        {
            User user =
                Mapper.Instance.Map<RegUserBm, User>(bind);

            this.Context.Users.Add(user);
            this.Context.SaveChanges();
        }

        public bool UserExists(LogUserBm bind)
        {
            var user = this.Context.Users
                .Any(u => u.Username == bind.Username &&
                          u.Password == bind.Password);

            return user;
        }

        public void LoginUser(LogUserBm bind, string sessionId)
        {
            if (!this.Context.Logins.Any(l => l.SessionId == sessionId))
            {
                Login newLogin = new Login() { SessionId = sessionId };
                this.Context.Logins.Add(newLogin);
                this.Context.SaveChanges();
            }

            Login myLogin = this.Context.Logins
                .FirstOrDefault(l => l.SessionId == sessionId);
            myLogin.IsActive = true;
            User model = this.Context.Users
                .FirstOrDefault(u => u.Username == bind.Username &&
                                     u.Password == bind.Password);
            myLogin.User = model;
            this.Context.SaveChanges();
        }
    }
}