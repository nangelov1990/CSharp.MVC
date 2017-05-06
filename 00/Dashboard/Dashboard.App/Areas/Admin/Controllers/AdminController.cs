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
    using Models.ViewModels.Request;
    using Services.AdminServices;

    [RouteArea("Admin"), RoutePrefix("")]
    [DashboardAuthorize(Roles = "Admin")]
    public class AdminsController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly AdminService _service;

        public AdminsController()
        {
            _service = new AdminService();
        }

        public AdminsController(ApplicationUserManager userManager)
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

        // GET: Admin/
        [Route("")]
        public ActionResult All()
        {
            IEnumerable<EmployeeListViewModel> viewModels =
                this._service.ListAdmins();
            if (viewModels == null)
            {
                return this.HttpNotFound();
            }

            return View(viewModels);
        }

        // GET: Admin/Add
        [Route("Add/")]
        public ActionResult Add()
        {
            return View();
        }
        
        // POST: Admin/Add
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
                    this.UserManager.AddToRole(user.Id, "Admin");
                    this._service.AddAdmin(model, user);

                    return RedirectToAction("All");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}