namespace CarDealerApp
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default", // Route name
                url: "{controller}/{action}/{make}", // URL with parameters
                defaults: new { controller = "Cars", action = "All", make = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapMvcAttributeRoutes();
        }
    }
}
