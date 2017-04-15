namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    
    using CarDealer.Models.ViewModels.Sales;
    using CarDealer.Services;

    [RoutePrefix("sales")]
    public class SalesController : CarDealerController
    {
        private new SalesService service;

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

            this.GetUsernameOfLoggedUser();

            return View(viewModels);
        }

        [HttpGet]
        [Route("{id:int}")]
        // GET: Sale details
        public ActionResult About(int id)
        {
            SaleVm viewModel =
                this.service.GetSale(id);

            this.GetUsernameOfLoggedUser();

            return View(viewModel);
        }

        [HttpGet]
        [Route("discounted/{percent?}")]
        // GET: Sales
        public ActionResult Discounted(double? percent)
        {
            IEnumerable<SaleVm> viewModels =
                this.service.GetDiscountedSales(percent);

            this.GetUsernameOfLoggedUser();

            return View(viewModels);
        }
    }
}