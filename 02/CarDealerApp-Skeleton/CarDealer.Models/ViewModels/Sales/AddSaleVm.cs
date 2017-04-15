namespace CarDealer.Models.ViewModels.Sales
{
    using System.Collections.Generic;
    using CarDealer.Models.ViewModels.Cars;
    using Customers;

    public class AddSaleVm
    {
        public IEnumerable<AddSaleCustomerVm> Customers { get; set; }
        public IEnumerable<AddSaleCarVm> Cars { get; set; }
    }
}