namespace Dashboard.App
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using AutoMapper;
    using Models.EntityModels;
    using Models.Enums;
    using Models.ViewModels.Request;
    using Models.ViewModels.Employee;

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
                expression.CreateMap<string, RequestStatus>();
                expression.CreateMap<Request, SingleRequestListView>();
                expression.CreateMap<Request, EditRequestViewModel>();
                expression.CreateMap<Admin, EmployeeListViewModel>()
                    .ForMember(vm => vm.Name, configureExpression =>
                        configureExpression.MapFrom(employee => employee.ApplicationUser.Name))
                    .ForMember(vm => vm.Email, configureExpression =>
                        configureExpression.MapFrom(employee => employee.ApplicationUser.Email))
                    .ForMember(vm => vm.Role, configureExpression =>
                        configureExpression.MapFrom(employee => employee.ApplicationUser.Roles.FirstOrDefault().RoleId.ToString()));
                expression.CreateMap<Employee, EmployeeListViewModel>()
                    .ForMember(vm => vm.Name, configureExpression =>
                        configureExpression.MapFrom(employee => employee.ApplicationUser.Name))
                    .ForMember(vm => vm.Email, configureExpression =>
                        configureExpression.MapFrom(employee => employee.ApplicationUser.Email))
                    .ForMember(vm => vm.Role, configureExpression =>
                        configureExpression.MapFrom(employee => employee.ApplicationUser.Roles.FirstOrDefault().RoleId.ToString()));
            });
        }
    }
}
