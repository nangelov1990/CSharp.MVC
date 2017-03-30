using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                name: "Suppliers by Type",
                url: "Suppliers/{type}",
                defaults: new { controller = "Suppliers", action = "ByType" }
            );

            routes.MapRoute(
                name: "Car Parts",
                url: "Cars/{id}/Parts",
                defaults: new { controller = "Cars", action = "Parts" }
            );

            routes.MapRoute(
                name: "Cars by Make",
                url: "Cars/{make}",
                defaults: new { controller = "Cars", action = "Make" }
            );

            routes.MapRoute(
                name: "Total Sales by Customer",
                url: "Customers/{id}",
                defaults: new { controller = "Customers", action = "TotalSales" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{parameter}",
                defaults: new { controller = "Home", action = "Index", parameter = UrlParameter.Optional }
            );
        }
    }
}
