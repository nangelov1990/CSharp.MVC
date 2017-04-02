namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using CarDealer.Models.ViewModels;
    using CarDealer.Services;

    [RoutePrefix("customers")]
    public class CustomersController : Controller
    {
        private CustomerService service;

        public CustomersController()
        {
            this.service = new CustomerService();
        }

        [HttpGet]
        [Route("all/{order:regex(ascending|descending)}")]
        // GET: Ordered Customers
        public ActionResult All(string order)
        {
            IEnumerable<AllCmrVm> viewModel =
                this.service.GetAllOrderedCustomers(order);

            return this.View(viewModel);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult About(int id)
        {
            CmrVm viewModel =
                this.service.GetCmrDetails(id);

            return this.View(viewModel);
        }
    }
}
