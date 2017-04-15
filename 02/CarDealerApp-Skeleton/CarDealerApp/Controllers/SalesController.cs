using CarDealerApp.Security;

namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using CarDealer.Models.BindingModels.Sales;
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
        [Route("{id:int}/")]
        // GET: Sale details
        public ActionResult About(int id)
        {
            SaleVm viewModel =
                this.service.GetSale(id);

            this.GetUsernameOfLoggedUser();

            return View(viewModel);
        }

        [HttpGet]
        [Route("discounted/{percent?}/")]
        // GET: Sales by percentage
        public ActionResult Discounted(double? percent)
        {
            IEnumerable<SaleVm> viewModels =
                this.service.GetDiscountedSales(percent);

            this.GetUsernameOfLoggedUser();

            return View(viewModels);
        }

        [HttpGet]
        [Route("add/")]
        // GET: Add Sale
        public ActionResult Add()
        {
            // Only logged users can add sales
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie == null ||
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            this.GetUsernameOfLoggedUser();
            AddSaleVm viewModel =
                this.service.AddSaleView();

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("add/review/")]
        // POST: Add Sale Review
        public ActionResult AddReview([Bind(Include = "CustomerId,CarId,Discount")] AddSaleBm bind)
        {
            // Only logged users can add sales
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie == null ||
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            this.GetUsernameOfLoggedUser();
            AddSaleReviewVm viewModel =
                this.service.AddSaleReviewView(bind);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("add/")]
        // POST: Add Sale
        public ActionResult Add([Bind(Include = "CustomerId,CarId,Discount")] AddSaleBm bind)
        {
            // Only logged users can add sales
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie == null ||
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            this.GetUsernameOfLoggedUser();
            this.service.AddSale(bind);

            return this.RedirectToAction("All");
        }
    }
}