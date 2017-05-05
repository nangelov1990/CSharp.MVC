namespace Dashboard.Models.EntityModels
{
    using System;
    using Enums;

    public class Request
    {
        public int Id { get; set; }

        public RequestType Type { get; set; }

        public RequestStatus Status { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public DateTime CreatedTime { get; set; }

        public RequestSolutionStep Step { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Owner { get; set; }

        public virtual Admin Distributor { get; set; }
    }
}
