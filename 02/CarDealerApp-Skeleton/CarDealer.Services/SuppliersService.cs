namespace CarDealer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using AutoMapper;

    using Models.EntityModels;
    using Models.ViewModels;
    using Models.ViewModels.Suppliers;

    public class SuppliersService : Service
    {
        public IEnumerable<AllSuppliersVm> GetAllSuppliersByType(string type)
        {
            type = type.ToLower();
            IEnumerable<Supplier> suppliers;
            if (type == "local")
            {
                suppliers = this.Context.Suppliers
                    .Where(s => s.IsImporter == false);
            } else if (type == "importers")
            {

                suppliers = this.Context.Suppliers
                    .Where(s => s.IsImporter == true);
            }
            else
            {
                throw new ArgumentException("The argument you have given for this order is invalid!");
            }

            IEnumerable<AllSuppliersVm> viewModels =
                Mapper.Instance.Map<IEnumerable<Supplier>, IEnumerable<AllSuppliersVm>>(suppliers);

            return viewModels;
        }
    }
}