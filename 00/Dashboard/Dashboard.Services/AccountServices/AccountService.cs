namespace Dashboard.Services.AccountServices
{
    using Models.BindingModels.Account;
    using Models.EntityModels;

    public class AccountService : Service
    {
        public void AddCustomer(RegisterCustomerBindingModel model, ApplicationUser appUser)
        {
            ApplicationUser user = this.Context.Users.Find(appUser.Id);
            Customer customer = new Customer()
            {
                ApplicationUser = user,
                CompanyName = model.CompanyName
            };
            this.Context.Customers.Add(customer);
            this.Context.SaveChanges();
        }
    }
}
