namespace CarDealer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Models.BindingModels;
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

        public void AddCustomer(CmrBm bind)
        {
            Customer cmr = Mapper.Instance.Map<CmrBm, Customer>(bind);
            if (DateTime.Now.Year - bind.BirthDate.Year < 21)
            {
                cmr.IsYoungDriver = true;
            }

            this.Context.Customers.Add(cmr);
            this.Context.SaveChanges();
        }

        public EditCmrVm ViewEditCmr(int id)
        {
            Customer cmr = this.Context.Customers.Find(id);
            EditCmrVm cmrVm = new EditCmrVm()
            {
                Id = cmr.Id,
                Name = cmr.Name,
                BirthDate = cmr.BirthDate
            };

            return cmrVm;
        }

        public void EditCmr(EditCmrBm bind)
        {
            Customer cmr = this.Context.Customers.Find(bind.Id);
            if (cmr == null)
            {
                throw new ArgumentException("Cannot find customer with such id!");
            }

            if (DateTime.Now.Year - bind.BirthDate.Year < 21)
            {
                cmr.IsYoungDriver = true;
            }
            else
            {
                cmr.IsYoungDriver = false;
            }

            cmr.Name = bind.Name;
            cmr.BirthDate = bind.BirthDate;
            this.Context.SaveChanges();
        }
    }
}
