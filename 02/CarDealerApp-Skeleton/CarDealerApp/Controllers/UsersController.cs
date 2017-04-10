namespace CarDealerApp.Controllers
{
    using System.Web.Mvc;

    using CarDealer.Models.BindingModels.Users;
    using CarDealer.Services;
    using Security;

    [RoutePrefix("users")]
    public class UsersController : Controller
    {
        private UserService service;

        public UsersController()
        {
            this.service = new UserService();
        }

        [HttpGet]
        [Route("register/")]
        // GET: Register User
        public ActionResult Register()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            var cookieExist = httpCookie != null;
            var loggedUser = AuthenticationManager.IsAuthenticated(httpCookie.Value);

            if (cookieExist &&
                loggedUser)
            {
                this.RedirectToAction("All", "Cars");
            }

            return View();
        }

        [HttpPost]
        [Route("register/")]
        // GET: Register User
        public ActionResult Register([Bind(Include="Username,Email,Password,ConfirmPassword")] RegUserBm bind)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            var cookieExist = httpCookie != null;
            var loggedUser = AuthenticationManager.IsAuthenticated(httpCookie.Value);
            if (cookieExist &&
                loggedUser)
            {
                this.RedirectToAction("All", "Cars");
            }

            var passwordMatch = bind.Password == bind.ConfirmPassword;
            if (this.ModelState.IsValid &&
                passwordMatch)
            {
                this.service.AddUser(bind);
                this.RedirectToAction("All", "Cars");
            }

            return this.RedirectToAction("Register");
        }
    }
}