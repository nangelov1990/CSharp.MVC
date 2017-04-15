using CarDealerApp.Security;

namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CarDealer.Models.BindingModels.Cars;
    using CarDealer.Models.ViewModels.Cars;
    using CarDealer.Services;

    [RoutePrefix("cars")]
    [Route("all")]
    public class CarsController : CarDealerController
    {
        private new CarsService service;

        public CarsController()
        {
            this.service = new CarsService();
        }

        [HttpGet]
        [Route("{make?}")]
        // GET: Cars
        public ActionResult All(string make)
        {
            IEnumerable<AllCarsByMakeVm> viewModels =
                this.service.GetCarsByMake(make);
            this.GetUsernameOfLoggedUser();

            return View(viewModels);
        }

        [HttpGet]
        [Route("{id:int}/parts")]
        // GET: Car with list of Parts
        public ActionResult About(int id)
        {
            AboutCarVm viewModels =
                this.service.GetCarWithParts(id);
            this.GetUsernameOfLoggedUser();

            return this.View(viewModels);
        }

        [HttpGet]
        [Route("add")]
        // GET: Add Car
        public ActionResult Add()
        {
            // Only logged users can add cars
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie == null ||
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            this.GetUsernameOfLoggedUser();

            return this.View();
        }

        [HttpPost]
        [Route("add")]
        // POST: Add Car
        public ActionResult Add([Bind(Include = "Make,Model,TravelledDistance,PartIds")] AddCarBm bind)
        {
            // Only logged users car add cars
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie == null &&
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (this.ModelState.IsValid)
            {
                this.GetUsernameOfLoggedUser();
                return this.Redirect("/cars");
            }

            return this.View();
        }
    }
}