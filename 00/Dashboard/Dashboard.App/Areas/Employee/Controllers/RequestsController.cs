namespace Dashboard.App.Areas.Employee.Controllers
{
    using System.Web.Mvc;
    using Attributes;
    using Models.ViewModels.Request;
    using Services.EmployeeServices;

    [RouteArea("Employee")]
    [RoutePrefix("Requests")]
    [DashboardAuthorize(Roles = "Admin, Employee")]
    public class RequestsController : Controller
    {
        private EmployeeRequestsService _service;

        public RequestsController()
        {
            this._service = new EmployeeRequestsService();
        }

        // GET: Employee/Requests
        [Route("")]
        public ActionResult All()
        {
            var username = HttpContext.User.Identity.Name;
            var viewModel =
                this._service.LoadRequests(username);

            // TODO: user friendly page
            if (viewModel == null)
                return this.HttpNotFound();

            return View(viewModel);
        }

        // GET: Employee/Requests/Open/5
        [Route("Open/{id}")]
        public ActionResult Open(int id)
        {
            var user = HttpContext.User;
            EditRequestViewModel viewModel =
                this._service.OpenRequest(id, user);

            if (viewModel == null)
                return this.HttpNotFound();

            return View(viewModel);
        }

        // POST: Employee/Requests/Accept/5
        [HttpPost]
        [Route("Accept/{id}")]
        public ActionResult Accept(int id)
        {
            var username = HttpContext.User.Identity.Name;
            this._service.AcceptRequest(id, username);

            return RedirectToAction("Open", new {id = id});
        }

        // POST: Employee/Requests/Accept/5
        [HttpPost]
        [Route("NextStep/{id}")]
        public ActionResult NextStep(int id)
        {
            var username = HttpContext.User.Identity.Name;
            this._service.NextStep(id, username);

            return RedirectToAction("Open", new {id = id});
        }

        [ChildActionOnly]
        [Route("LoadAcceptButton")]
        public ActionResult LoadAcceptButton(int id)
        {
            var username = HttpContext.User.Identity.Name;
            var RequestPending = this._service.IsRequestPedning(id, username);

            if (RequestPending)
            {
                return PartialView("_AcceptButton");
            }

            return null;
        }

        [ChildActionOnly]
        [Route("LoadNextStepButton")]
        public ActionResult LoadNextStepButton(int id)
        {
            var username = HttpContext.User.Identity.Name;
            var requestHasMoreSteps = this._service.HasMoreSteps(id, username);

            if (requestHasMoreSteps)
            {
                return PartialView("_NextStepButton");
            }

            return null;
        }
    }
}
