﻿namespace CarDealer.Services
{
    using System.Collections.Generic;

    using AutoMapper;

    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels;

    public class PartsService : Service
    {
        public IEnumerable<AllPartVm> AllParts()
        {
            IEnumerable<Part> parts = this.Context.Parts;

            IEnumerable<AllPartVm> viewModels =
                Mapper.Instance.Map<IEnumerable<Part>, IEnumerable<AllPartVm>>(parts);

            return viewModels;
        }

        public IEnumerable<AddPartSupplierVm> LoadSuppliers()
        {
            IEnumerable<Supplier> suppliers = this.Context.Suppliers;

            IEnumerable<AddPartSupplierVm> suppliersVm =
                Mapper.Instance.Map<IEnumerable<Supplier>, IEnumerable<AddPartSupplierVm>>(suppliers);

            return suppliersVm;
        }

        public void AddPart(PartBm bind)
        {
            Supplier supplier =
                this.Context.Suppliers.Find(bind.SupplierId);
            Part part = new Part()
            {

                Name = bind.Name,
                Price = bind.Price,
                Quantity = bind.Quantity,
                Supplier = supplier
            };

            this.Context.Parts.Add(part);
            this.Context.SaveChanges();
        }

        public EditPartVm ViewEditPart(int id)
        {
            Part part = this.Context.Parts.Find(id);
            EditPartVm partVm =
                Mapper.Instance.Map<Part, EditPartVm>(part);

            return partVm;
        }
    }
}