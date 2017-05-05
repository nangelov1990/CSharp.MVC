namespace Dashboard.App.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Attributes;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models.BindingModels.Employee;
    using Models.EntityModels;
    using Models.ViewModels.Employee;
    using Services.AdminServices;

    [RouteArea("Admin"), RoutePrefix("Employees")]
    [DashboardAuthorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly AdminEmployeeService _service;

        public EmployeesController()
        {
            _service = new AdminEmployeeService();
        }

        public EmployeesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: Admin/Employees
        [Route("")]
        public ActionResult All()
        {
            IEnumerable<EmployeeListViewModel> viewModels =
                this._service.ListEmployees();
            if (viewModels == null)
            {
                return this.HttpNotFound();
            }

            return View(viewModels);
        }

        // GET: Admin/Employees/Add
        [Route("Add/")]
        public ActionResult Add()
        {
            return View();
        }
        
        // POST: Admin/Employees/Add
        [HttpPost]
        [Route("Add/")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Email, Name, Password, ConfirmPassword")] AddEmployeeBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    this.UserManager.AddToRole(user.Id, "Employee");
                    this._service.AddEmployee(model, user);

                    return RedirectToAction("All");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}