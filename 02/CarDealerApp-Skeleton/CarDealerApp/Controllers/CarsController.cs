namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using CarDealer.Models.BindingModels;
    using CarDealer.Models.BindingModels.Cars;
    using CarDealer.Models.ViewModels;
    using CarDealer.Models.ViewModels.Cars;
    using CarDealer.Services;

    [RoutePrefix("cars")]
    [Route("all")]
    public class CarsController : Controller
    {
        private CarsService service;

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

            return View(viewModels);
        }

        [HttpGet]
        [Route("{id:int}/parts")]
        // GET: Car with list of Parts
        public ActionResult About(int id)
        {
            AboutCarVm viewModels =
                this.service.GetCarWithParts(id);

            return this.View(viewModels);
        }

        [HttpGet]
        [Route("add")]
        // GET: Add Car
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Route("add")]
        // POST: Add Car
        public ActionResult Add([Bind(Include = "Make,Model,TravelledDistance,PartIds")] AddCarBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddCar(bind);

                return this.Redirect("/cars");
            }
            return this.View();
        }
    }
}