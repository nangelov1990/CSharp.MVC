namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using CarDealer.Models.ViewModels;
    using CarDealer.Services;

    [RoutePrefix("cars")]
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
    }
}