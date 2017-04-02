namespace CarDealer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;

    using Models.EntityModels;
    using Models.ViewModels;

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
    }
}