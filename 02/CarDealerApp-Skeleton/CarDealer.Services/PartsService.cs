namespace CarDealer.Services
{
    using System.Collections.Generic;

    using AutoMapper;

    using Models.BindingModels;
    using Models.BindingModels.Parts;
    using Models.EntityModels;
    using Models.ViewModels;
    using Models.ViewModels.Parts;
    using Models.ViewModels.Suppliers;

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

        public DeletePartVm DeletePartView(int id)
        {
            Part part = this.Context.Parts.Find(id);
            DeletePartVm partVm =
                Mapper.Instance.Map<Part, DeletePartVm>(part);

            return partVm;
        }

        public void DeletePart(int id)
        {
            Part part = this.Context.Parts.Find(id);
            this.Context.Parts.Remove(part);
            this.Context.SaveChanges();
        }

        public EditPartVm ViewEditPart(int id)
        {
            Part part = this.Context.Parts.Find(id);
            EditPartVm partVm =
                Mapper.Instance.Map<Part, EditPartVm>(part);

            return partVm;
        }

        public void EditPart(EditPartBm bind)
        {
            Part part = this.Context.Parts.Find(bind.Id);
            part.Quantity = bind.Quantity;
            part.Price = bind.Price;

            this.Context.SaveChanges();
        }
    }
}