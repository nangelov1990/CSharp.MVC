namespace CarDealer.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    
    using AutoMapper;
    using Models.BindingModels.Suppliers;
    using Models.EntityModels;
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

        public void AddSupplier(SupplierBm bind)
        {
            Supplier model = new Supplier()
            {
                Name = bind.Name,
                IsImporter = bind.Type
            };

            this.Context.Suppliers.Add(model);
            this.Context.SaveChanges();
        }

        public SupplierVm EditModelView(int id)
        {
            Supplier supplier = this.Context.Suppliers.Find(id);
            SupplierVm viewModel =
                Mapper.Instance.Map<Supplier, SupplierVm>(supplier);

            return viewModel;
        }

        public void EditModel(SupplierBm bind)
        {
            Supplier supplier = this.Context.Suppliers.Find(bind.Id);
            supplier.Name = bind.Name;
            supplier.IsImporter = bind.Type;

            this.Context.SaveChanges();
        }

        public SupplierVm DeleteModelView(int id)
        {
            Supplier supplier = this.Context.Suppliers.Find(id);
            SupplierVm viewModel =
                Mapper.Instance.Map<Supplier, SupplierVm>(supplier);

            return viewModel;
        }

        public void DeleteModel(SupplierBm bind)
        {

            Supplier supplier = this.Context.Suppliers.Find(bind.Id);
            IEnumerable<Part> partsFromSupplier = this.Context.Parts
                .Where(p => p.Supplier.Id == supplier.Id);

            this.Context.Parts.RemoveRange(partsFromSupplier);
            this.Context.Suppliers.Remove(supplier);

            this.Context.SaveChanges();
        }
    }
}