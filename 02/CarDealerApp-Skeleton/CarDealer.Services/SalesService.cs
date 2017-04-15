namespace CarDealer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.BindingModels.Sales;
    using Models.EntityModels;
    using Models.ViewModels;
    using Models.ViewModels.Cars;
    using Models.ViewModels.Customers;
    using Models.ViewModels.Sales;

    public class SalesService : Service
    {
        public IEnumerable<SaleVm> GetAllSales()
        {
            IEnumerable<Sale> sales = this.Context.Sales;

            IEnumerable<SaleVm> salesVm =
                Mapper.Instance.Map<IEnumerable<Sale>, IEnumerable<SaleVm>>(sales);

            return salesVm;
        }

        public SaleVm GetSale(int id)
        {
            Sale sale = this.Context.Sales.Find(id);

            SaleVm saleVm = Mapper.Instance.Map<Sale, SaleVm>(sale);

            return saleVm;
        }

        public IEnumerable<SaleVm> GetDiscountedSales(double? percent)
        {
            percent /= 100;
            IEnumerable<Sale> sales = this.Context.Sales
                .Where(s => s.Discount != 0);
            if (percent != null)
            {
                sales = sales.Where(s => s.Discount == percent.Value);
            }

            IEnumerable<SaleVm> salesVm =
                Mapper.Instance.Map<IEnumerable<Sale>, IEnumerable<SaleVm>>(sales);

            return salesVm;
        }

        public AddSaleVm AddSaleView()
        {
            IEnumerable<Customer> cmrs = this.Context.Customers;
            IEnumerable<Car> cars = this.Context.Cars;

            IEnumerable<AddSaleCustomerVm> cmrsVm =
                Mapper.Instance.Map<IEnumerable<Customer>, IEnumerable<AddSaleCustomerVm>>(cmrs);
            IEnumerable<AddSaleCarVm> carsVm =
                Mapper.Instance.Map<IEnumerable<Car>, IEnumerable<AddSaleCarVm>>(cars);

            AddSaleVm viewModel = new AddSaleVm()
            {
                Customers = cmrsVm,
                Cars = carsVm
            };

            return viewModel;
        }

        public AddSaleReviewVm AddSaleReviewView(AddSaleBm bind)
        {
            Customer cmr = this.Context.Customers.Find(bind.CustomerId);
            Car car = this.Context.Cars.Find(bind.CarId);

            AddSaleCustomerVm cmrVm =
                Mapper.Instance.Map<Customer, AddSaleCustomerVm>(cmr);
            AddSaleCarVm carVm =
                Mapper.Instance.Map<Car, AddSaleCarVm>(car);

            AddSaleReviewVm viewModel = new AddSaleReviewVm()
            {
                Customer = cmrVm,
                Car = carVm,
                Discount = bind.Discount,
                CarPrice = car.Parts.Sum(p => p.Price).Value
            };

            return viewModel;
        }

        public void AddSale(AddSaleBm bind)
        {
            Customer cmr = this.Context.Customers.Find(bind.CustomerId);
            Car car = this.Context.Cars.Find(bind.CarId);
            var discount = bind.Discount / 100;

            Sale sale = new Sale()
            {
                Customer = cmr,
                Car = car,
                Discount = discount
            };
            this.Context.Sales.Add(sale);
            this.Context.SaveChanges();
        }
    }
}