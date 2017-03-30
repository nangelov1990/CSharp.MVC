using System.Web.Mvc;
using System.Web.Routing;

namespace CarDealerApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Sale discounted",
              url: "Sales/discounted/{percent}/",
              defaults: new { controller = "Sales", action = "Discounted", percent = UrlParameter.Optional}
          );

            routes.MapRoute(
                name: "Sale info",
                url: "Sales/{id}/",
                defaults: new { controller = "Sales", action = "About" }
            );

            routes.MapRoute(
                name: "All sales",
                url: "Sales/",
                defaults: new { controller = "Sales", action = "All" }
                );

            ;

            routes.MapRoute(
                name: "Cars with list of parts",
                url: "cars/{id}/parts/",
                defaults: new { controller = "Cars", action = "About" },
                constraints: new { id = @"\d+" }
                );

            routes.MapRoute(
                name: "Suppliers filtered",
                url: "suppliers/{type}/",
                defaults: new { controller = "Suppliers", action = "All" },
                constraints: new { type = @"importers|local" }
                );


            routes.MapRoute(
                name: "Customers ordered",
                url: "customers/all/{order}/",
                defaults: new { controller = "Customers", action = "All", order = "ascending" },
                constraints: new { order = @"ascending|descending" });

            routes.MapRoute(
                name: "Total sales by Customers",
                url: "customers/{id}/",
                 defaults: new { controller = "Customers", action = "About" },
                constraints: new { id = @"\d+" }
                );

            routes.MapRoute(
                name: "Cars by make",
                url: "cars/{make}/",
                defaults: new { controller = "Cars", action = "All" });
        }
    }
}
