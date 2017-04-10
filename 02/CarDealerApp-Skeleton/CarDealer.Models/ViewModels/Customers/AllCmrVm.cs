namespace CarDealer.Models.ViewModels.Customers
{
    using System;

    public class AllCmrVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }
    }
}