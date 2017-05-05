namespace Dashboard.Models.BindingModels.Request
{
    public class AssignEmployeeToRequestBindingModel
    {
        public int RequestId { get; set; }

        public string DistributorUsername { get; set; }

        public int EmployeeId { get; set; }
    }
}
