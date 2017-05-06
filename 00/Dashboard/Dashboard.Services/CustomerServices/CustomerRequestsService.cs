namespace Dashboard.Services.CustomerServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.BindingModels.Request;
    using Models.EntityModels;
    using Models.Enums;
    using Models.ViewModels.Request;

    public class CustomerRequestsService : Service
    {
        public void AddCustomerRequest(AddRequestBindingModel model, string userName)
        {
            RequestStatus status = RequestStatus.New;
            DateTime createdTime = DateTime.Now;
            ApplicationUser user = this.Context.Users.FirstOrDefault(appuser => appuser.UserName == userName);
            Customer customer = this.Context.Customers.FirstOrDefault(cust => cust.ApplicationUser.Id == user.Id);
            Request request = new Request()
            {
                Type = model.Type,
                Status = status,
                Name = model.Name,
                Message = model.Message,
                CreatedTime = createdTime,
                Customer = customer
            };

            this.Context.Requests.Add(request);
            this.Context.SaveChanges();
        }

        public IEnumerable<SingleRequestListView> LoadCustomerRequests(string status, string userName)
        {
            IEnumerable<Request> requests =
                this.Context.Requests.Where(r => r.Customer.ApplicationUser.UserName == userName);
            if (!string.IsNullOrEmpty(status))
            {
                RequestStatus? requestStatus = ValidateRequestStatus(status);
                if (requestStatus == null)
                {
                    return null;
                }

                requests = requests.Where(req => req.Status == requestStatus);
            }
            
            ICollection<SingleRequestListView> viewModel = new HashSet<SingleRequestListView>();
            foreach (Request request in requests)
            {
                SingleRequestListView singleRequestViewModel = Mapper.Instance.Map<Request, SingleRequestListView>(request);
                viewModel.Add(singleRequestViewModel);
            }

            return viewModel;
        }

        private static RequestStatus? ValidateRequestStatus(string status)
        {
            RequestStatus validatedStatus;
            if (Enum.TryParse(status, out validatedStatus))
            {
                if (validatedStatus.ToString() == status)
                {
                    return validatedStatus;
                }
            }

            return null;
        }
    }
}
