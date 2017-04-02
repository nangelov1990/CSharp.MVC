namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CarDealer.Models.ViewModels;
    using CarDealer.Services;

    [RoutePrefix("sales")]
    public class SalesController : Controller
    {
        private SalesService service;

        public SalesController()
        {
            this.service = new SalesService();
        }

        [HttpGet]
        [Route]
        // GET: Sales
        public ActionResult All()
        {
            IEnumerable<SaleVm> viewModels =
                this.service.GetAllSales();

            return View(viewModels);
        }

        [HttpGet]
        [Route("{id:int}")]
        // GET: Sale details
        public ActionResult About(int id)
        {
            SaleVm viewModel =
                this.service.GetSale(id);

            return View(viewModel);
        }

        [HttpGet]
        [Route("discounted/{percent?}")]
        // GET: Sales
        public ActionResult Discounted(double? percent)
        {
            IEnumerable<SaleVm> viewModels =
                this.service.GetDiscountedSales(percent);

            return View(viewModels);
        }
    }
}