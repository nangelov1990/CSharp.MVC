namespace Dashboard.Models.ViewModels.Request
{
    using EntityModels;
    using Enums;

    public class SingleRequestListView
    {
        public int Id { get; set; }

        public RequestType Type { get; set; }

        public RequestStatus Status { get; set; }

        public string Name { get; set; }

        public virtual Employee Owner { get; set; }

        public bool Assigned => this.Status != RequestStatus.New;
    }
}
