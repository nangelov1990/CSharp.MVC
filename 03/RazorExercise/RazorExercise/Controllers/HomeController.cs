
namespace RazorExercise.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Services.Description;
    using Models;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IndexVm vm = new IndexVm()
            {
                Title = "Index",
                Message = "Razor Exercise Index Page",
                People = this._people
            };

            return View(vm);
        }

        public ActionResult About()
        {
            AboutVm vm = new AboutVm()
            {
                Title = "About",
                Message = "Your application description page.",
                NumberOfUsers = this._people.Length,
                Resources = new List<string> { "Ivan", "Stoyan" },
                People = this._people
            };

            return View(vm);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private readonly Person[] _people = new Person[]
        {
            new Person
            {
                Name = "Ivan",
                Age = 15
            },
            new Person
            {
                Name = "Stoyan",
                Age = 20
            },
            new Person
            {
                Name = "Hector",
                Age = 25
            },
            new Person
            {
                Name = "Chaves",
                Age = 30
            },
        };
    }
}