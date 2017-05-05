namespace Dashboard.Services.EmployeeServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using AutoMapper;
    using Models.EntityModels;
    using Models.Enums;
    using Models.ViewModels.Request;

    public class EmployeeRequestsService : Service
    {
        public IEnumerable<SingleRequestListView> LoadRequests(string username)
        {
            Employee owner = this.Context.Employees
                .FirstOrDefault(e => e.ApplicationUser.UserName == username);
            IEnumerable<Request> requests = this.Context.Requests.Where(r => r.Owner.Id == owner.Id);
            if (requests.Any())
            {
                ICollection<SingleRequestListView> viewModel = new HashSet<SingleRequestListView>();
                foreach (Request request in requests)
                {
                    SingleRequestListView singleRequestViewModel =
                        Mapper.Instance.Map<Request, SingleRequestListView>(request);
                    viewModel.Add(singleRequestViewModel);
                }

                return viewModel;
            }

            return null;
        }

        public EditRequestViewModel OpenRequest(int id, IPrincipal user)
        {
            Request request = this.Context.Requests.Find(id);
            var requestExist = request != null;
            if (requestExist)
            {
                var userIsAdmin = user.IsInRole("Admin");
                if (!userIsAdmin)
                {
                    Employee employee = this.Context.Employees
                        .FirstOrDefault(e => e.ApplicationUser.UserName == user.Identity.Name);
                    var employeeExists = employee != null;
                    if (employeeExists)
                    {
                        var employeeIsOwner =
                            request.Owner.Id == employee.Id;
                        if (!employeeIsOwner)
                        {
                            return null;
                        }
                    }
                }

                EditRequestViewModel viewModel =
                    Mapper.Instance.Map<Request, EditRequestViewModel>(request);
                return viewModel;
            }

            return null;
        }

        public void AcceptRequest(int id, string username)
        {
            Employee owner = this.Context.Employees
                .FirstOrDefault(e => e.ApplicationUser.UserName == username);
            Request request = this.Context.Requests
                .Where(r => r.Owner.Id == owner.Id)
                .FirstOrDefault(r => r.Id == id);
            bool requestExists = request != null;
            if (requestExists)
            {
                request.Status = RequestStatus.Accepted;

                this.Context.SaveChanges();
            }
        }

        public void NextStep(int id, string username)
        {
            Employee owner = this.Context.Employees
                .FirstOrDefault(e => e.ApplicationUser.UserName == username);
            Request request = this.Context.Requests
                .Where(r => r.Owner.Id == owner.Id)
                .FirstOrDefault(r => r.Id == id);
            bool requestExists = request != null;
            if (requestExists)
            {
                request.Step = ++request.Step;
                if (request.Step == RequestSolutionStep.CloseRequest)
                    request.Status = RequestStatus.Completed;

                this.Context.SaveChanges();
            }
        }

        public bool IsRequestPedning(int id, string username)
        {
            Employee owner = this.Context.Employees
                .FirstOrDefault(e => e.ApplicationUser.UserName == username);
            Request request = this.Context.Requests
                .Where(r => r.Owner.Id == owner.Id)
                .FirstOrDefault(r => r.Id == id);
            bool requestExists = request != null;
            if (requestExists)
            {
                return request.Status <= RequestStatus.Pending;
            }

            return false;
        }

        public bool HasMoreSteps(int id, string username)
        {
            Employee owner = this.Context.Employees
                .FirstOrDefault(e => e.ApplicationUser.UserName == username);
            Request request = this.Context.Requests
                .Where(r => r.Owner.Id == owner.Id)
                .FirstOrDefault(r => r.Id == id);
            bool requestExists = request != null;
            if (requestExists)
            {
                var hasMoreSteps = request.Step != RequestSolutionStep.CloseRequest;
                var isAccepted = request.Status == RequestStatus.Accepted;
                var canProgress = hasMoreSteps && isAccepted;

                return canProgress;
            }

            return false;
        }
    }
}
