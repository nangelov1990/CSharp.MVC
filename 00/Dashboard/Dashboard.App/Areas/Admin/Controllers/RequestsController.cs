namespace Dashboard.App.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Attributes;
    using Models.BindingModels.Request;
    using Models.ViewModels.Request;
    using Services.AdminServices;

    [RouteArea("Admin")]
    [RoutePrefix("Requests")]
    [DashboardAuthorize(Roles = "Admin")]
    public class RequestsController : Controller
    {

        private AdminRequestsService _service;

        public RequestsController()
        {
            this._service = new AdminRequestsService();
        }

        [Route("")]
        // GET: Admin/Requests
        public ActionResult All()
        {
            IEnumerable<SingleRequestListView> viewModel =
                   this._service.LoadAllRequests();

            // TODO: user friendly page
            if (viewModel == null)
                return this.HttpNotFound();

            return View(viewModel);
        }

        [Route("Assign/{id}")]
        // GET: Admin/Requests/Assign/5
        public ActionResult Assign(int id)
        {
            string distributorUsername = HttpContext.User.Identity.Name;

            AssignEmployeeToRequestViewModel viewModel =
                this._service.AssignRequestToEmployee(id, distributorUsername);

            return View(viewModel);
        }

        // POST: Admin/Requests/Assign/5
        [HttpPost]
        [Route("Assign/{id}")]
        public ActionResult Assign(int id, [Bind(Include ="RequestId, DistributorUsername, EmployeeId")] AssignEmployeeToRequestBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Assign", new { id = id });
            }

            this._service.AssignEmployeeToRequest(model);
            return RedirectToAction("All");
        }

        [ChildActionOnly]
        [Route("LoadButtons/{request}")]
        public ActionResult LoadButtons(SingleRequestListView request)
        {
            ViewBag.RequestId = request.Id;
            if (request.Assigned)
            {
                return PartialView("_OpenButton");
            }
            else
            {
                return PartialView("_AssignOpenButtons");
            }
        }
    }
}
