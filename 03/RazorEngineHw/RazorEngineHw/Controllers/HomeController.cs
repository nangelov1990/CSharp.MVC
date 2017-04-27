namespace RazorEngineHw.Controllers
{
    using System.Web.Mvc;
    using Data;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Users()
        {
            var viewModel = Db.People;

            return this.View(viewModel);
        }
    }
}