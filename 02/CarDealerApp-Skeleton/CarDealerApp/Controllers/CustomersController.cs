namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CarDealer.Models.BindingModels.Customers;
    using CarDealer.Models.ViewModels.Customers;
    using CarDealer.Services;

    [RoutePrefix("customers")]
    public class CustomersController : CarDealerController
    {
        private new CustomerService service;

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

            this.GetUsernameOfLoggedUser();

            return this.View(viewModel);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult About(int id)
        {
            CmrVm viewModel =
                this.service.GetCmrDetails(id);

            this.GetUsernameOfLoggedUser();

            return this.View(viewModel);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            this.GetUsernameOfLoggedUser();

            return this.View();
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Name,BirthDate")] CmrBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddCustomer(bind);

                return this.RedirectToAction("All", new {order = "ascending"});
            }
            
            return this.View();
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            EditCmrVm viewModel =
                this.service.ViewEditCmr(id);

            this.GetUsernameOfLoggedUser();

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id,Name,BirthDate")] EditCmrBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditCmr(bind);

                return this.RedirectToAction("All", new {order = "ascending"});
            }

            return this.View();
        }
    }
}
