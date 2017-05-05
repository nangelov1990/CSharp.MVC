namespace Dashboard.Services.AdminServices
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.BindingModels.Employee;
    using Models.EntityModels;
    using Models.ViewModels.Employee;

    public class AdminEmployeeService : Service
    {
        public void AddEmployee(AddEmployeeBindingModel model, ApplicationUser appUser)
        {
            ApplicationUser user = this.Context.Users.Find(appUser.Id);
            Employee employee = new Employee()
            {
                ApplicationUser = user
            };
            this.Context.Employees.Add(employee);
            this.Context.SaveChanges();
        }

        public IEnumerable<EmployeeListViewModel> ListEmployees()
        {
            var employees = this.Context.Employees;
            IEnumerable<EmployeeListViewModel> viewModels =
                Mapper.Instance.Map<IEnumerable<Employee>, IEnumerable<EmployeeListViewModel>>(employees);
            foreach (var employeeListViewModel in viewModels)
            {
                var roleName = this.Context.Roles.Find(employeeListViewModel.Role).Name;
                employeeListViewModel.Role = roleName;
            }

            return viewModels;
        }
    }
}
