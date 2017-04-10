namespace CarDealer.Services
{
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
    }
}