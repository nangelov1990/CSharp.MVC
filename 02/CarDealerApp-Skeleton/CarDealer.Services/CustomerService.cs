namespace CarDealer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Models.EntityModels;
    using Models.ViewModels;

    public class CustomerService : Service
    {
        public IEnumerable<AllCmrVm> GetAllOrderedCustomers(string order)
        {
            IEnumerable<Customer> customers;
            order = order.ToLower();
            if (order == "ascending")
            {
                customers =
                    this.Context.Customers
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver);
            }
            else if (order == "descending")
            {
                customers =
                    this.Context.Customers
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver);
            }
            else
            {
                throw new ArgumentException("The argument you have given for the order is invalid!");
            }

            IEnumerable<AllCmrVm> viewModels =
                Mapper.Instance.Map<IEnumerable<Customer>, IEnumerable<AllCmrVm>>(customers);

            return viewModels;
        }

        public CmrVm GetCmrDetails(int id)
        {
            Customer cmr = this.Context.Customers.Find(id);

            CmrVm cmrVm = new CmrVm()
            {
                Name = cmr.Name,
                BoughtCarsCount = cmr.Sales.Count,
                TotalMoneySpent = cmr.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price))
            };

            return cmrVm;
        }
    }
}
