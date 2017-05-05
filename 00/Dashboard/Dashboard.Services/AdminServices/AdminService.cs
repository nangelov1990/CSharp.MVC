namespace Dashboard.Services.AdminServices
{
    using System.Collections.Generic;
    using AutoMapper;
    using Models.BindingModels.Employee;
    using Models.EntityModels;
    using Models.ViewModels.Employee;

    public class AdminService : Service
    {
        public void AddAdmin(AddEmployeeBindingModel model, ApplicationUser appUser)
        {
            ApplicationUser user = this.Context.Users.Find(appUser.Id);
            Admin admin = new Admin()
            {
                ApplicationUser = user
            };
            this.Context.Admins.Add(admin);
            this.Context.SaveChanges();
        }

        public IEnumerable<EmployeeListViewModel> ListAdmins()
        {
            var employees = this.Context.Admins;
            IEnumerable<EmployeeListViewModel> viewModels =
                Mapper.Instance.Map<IEnumerable<Admin>, IEnumerable<EmployeeListViewModel>>(employees);
            foreach (var employeeListViewModel in viewModels)
            {
                var roleName = this.Context.Roles.Find(employeeListViewModel.Role).Name;
                employeeListViewModel.Role = roleName;
            }

            return viewModels;
        }
    }
}
