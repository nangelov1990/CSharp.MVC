namespace Dashboard.Services.AdminServices
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.BindingModels.Request;
    using Models.EntityModels;
    using Models.Enums;
    using Models.ViewModels.Request;

    public class AdminRequestsService : Service
    {
        public IEnumerable<SingleRequestListView> LoadAllRequests()
        {
            IEnumerable<Request> requests = this.Context.Requests;
            ICollection<SingleRequestListView> viewModel = new HashSet<SingleRequestListView>();
            foreach (Request request in requests)
            {
                SingleRequestListView singleRequestViewModel =
                    Mapper.Instance.Map<Request, SingleRequestListView>(request);
                viewModel.Add(singleRequestViewModel);
            }

            return viewModel;
        }

        public AssignEmployeeToRequestViewModel AssignRequestToEmployee(int id, string distributorUsername)
        {

            var roleId = this.Context.Roles.FirstOrDefault(r => r.Name == "Employee").Id;
            IEnumerable<Employee> employees = this.Context.Employees
                .Where(e =>
                    e.ApplicationUser.Roles.Any(r =>
                        r.RoleId == roleId));
            ICollection<EmployeeToBeAssignedViewModel> employeesCollection =
                new HashSet<EmployeeToBeAssignedViewModel>();
            foreach (var employee in employees)
            {
                var vm = new EmployeeToBeAssignedViewModel()
                {
                    Id = employee.Id,
                    Username = employee.ApplicationUser.UserName
                };
                employeesCollection.Add(vm);
            }

            IEnumerable<EmployeeToBeAssignedViewModel> employeesViewModel = employeesCollection;
            AssignEmployeeToRequestViewModel viewModel = new AssignEmployeeToRequestViewModel()
            {
                RequestId = id,
                DistributorUsername = distributorUsername,
                Employees = employeesViewModel
            };

            return viewModel;
        }

        public void AssignEmployeeToRequest(AssignEmployeeToRequestBindingModel model)
        {
            Request request = this.Context.Requests.Find(model.RequestId);
            Employee owner = this.Context.Employees.Find(model.EmployeeId);
            Admin distributor = this.Context.Admins.FirstOrDefault(a => a.ApplicationUser.UserName == model.DistributorUsername);

            var requestExists = request != null;
            var ownerExists = owner != null;
            var distributorExists = distributor != null;
            var allEntitiesExist = requestExists && ownerExists && distributorExists;

            if (allEntitiesExist)
            {
                request.Owner = owner;
                request.Distributor = distributor;
                request.Status = RequestStatus.Pending;

                this.Context.SaveChanges();
            }
        }

        public bool IsRequestAssigned(int id)
        {
            Request request = this.Context.Requests
                .Find(id);
            bool requestExists = request != null;
            if (requestExists)
            {
                var isAssigned = request.Status != RequestStatus.New;

                return isAssigned;
            }

            return false;
        }
    }
}
