namespace CarDealer.Models.ViewModels.Sales
{
    using Cars;
    using Customers;

    public class SaleVm
    {
        public CarVm Car { get; set; }
        public CmrVm Customer { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}