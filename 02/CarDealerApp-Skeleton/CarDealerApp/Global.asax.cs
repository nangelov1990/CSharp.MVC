namespace CarDealerApp
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using AutoMapper;
    using CarDealer.Models.EntityModels;
    using CarDealer.Models.ViewModels;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.RegisterMaps();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterMaps()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Customer, AllCmrVm>();
                expression.CreateMap<Car, AllCarsByMakeVm>();
                expression.CreateMap<Supplier, AllSuppliersVm>()
                .ForMember(vm => vm.NumberOfPartsToSupply,
                    configExpression =>
                        configExpression.MapFrom(supplier => supplier.Parts.Count));
                expression.CreateMap<Car, CarVm>();
                expression.CreateMap<Part, PartVm>();
                expression.CreateMap<Customer, CmrVm>();
                expression.CreateMap<Sale, SaleVm>()
                    .ForMember(vm => vm.Price,
                        configExpression =>
                        configExpression.MapFrom(s => s.Car.Parts.Sum(p => p.Price)));
            });
        }
    }
}
