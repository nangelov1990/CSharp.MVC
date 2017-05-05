namespace Dashboard.Models.ViewModels.Request
{
    using System.Collections.Generic;

    public class AssignEmployeeToRequestViewModel
    {
        public int RequestId { get; set; }

        public string DistributorUsername { get; set; }

        public IEnumerable<EmployeeToBeAssignedViewModel> Employees { get; set; }
    }
}
