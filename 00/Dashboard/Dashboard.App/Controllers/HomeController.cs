namespace Dashboard.App.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "SoftUni C# MVC Coursework - 2017";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Nikola Angelov.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult LoadNavigationButtons()
        {
            var identityIsAuthenticated = this.HttpContext.User.Identity.IsAuthenticated;
            if (identityIsAuthenticated)
            {
                var admin = this.User.IsInRole("Admin");
                var employee = this.User.IsInRole("Employee");
                var customer = this.User.IsInRole("Customer");

                if (admin)
                {
                    return this.PartialView("_AdminNavigation");
                }

                if (employee)
                {
                    return this.PartialView("_EmployeeNavigation");
                }

                if (customer)
                {
                    return this.PartialView("_CustomerNavigation");
                }
            }

            return null;
        }
    }
}