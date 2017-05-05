namespace Dashboard.App.Areas.Customer.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Attributes;
    using Models.BindingModels.Request;
    using Models.ViewModels.Request;
    using Services.CustomerServices;

    [RouteArea("Customer")]
    [RoutePrefix("Requests")]
    [DashboardAuthorize]
    public class RequestsController : Controller
    {
        private CustomerRequestsService _service;

        public RequestsController()
        {
            this._service = new CustomerRequestsService();
        }

        // GET: Requests
        [Route("")]
        public ActionResult All(string status)
        {
            IEnumerable<SingleRequestListView> viewModel = 
                this._service.LoadCustomerRequests(status, HttpContext.User.Identity.Name);

            // TODO: user friendly page
            if (viewModel == null)
                return this.HttpNotFound();

            return View(viewModel);
        }

        // GET: Requests/Add
        [Route("Add/")]
        public ActionResult Add()
        {
            return View();
        }

        // POST: Requests/Add
        [HttpPost]
        [Route("Add/")]
        public ActionResult Add([Bind(Include = "Type, Name, Message")] AddRequestBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = this.HttpContext.User.Identity.Name;
                this._service.AddCustomerRequest(model, userName);

                return RedirectToAction("All");
            }

            return View(model);
        }
    }
}
