namespace CarDealer.Models.ViewModels.Cars
{
    public class AllCarsByMakeVm
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public long TravelledDistance { get; set; }
    }
}