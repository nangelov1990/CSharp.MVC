namespace Dashboard.Models.ViewModels.Request
{
    using System;
    using System.ComponentModel;
    using EntityModels;
    using Enums;

    public class EditRequestViewModel
    {

        public int Id { get; set; }

        public RequestType Type { get; set; }

        public RequestStatus Status { get; set; }

        public virtual Admin Distributor { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        [DisplayName("Created")]
        public DateTime CreatedTime { get; set; }

        public RequestSolutionStep Step { get; set; }

        public RequestSolutionStep? NextStep { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Owner { get; set; }
    }
}
