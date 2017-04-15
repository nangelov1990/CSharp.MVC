namespace CarDealerApp.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    using CarDealer.Models.BindingModels.Users;
    using CarDealer.Services;
    using Security;

    [RoutePrefix("users")]
    public class UsersController : CarDealerController
    {
        private new UserService service;

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
            if (httpCookie != null &&
                AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            this.GetUsernameOfLoggedUser();

            return View();
        }

        [HttpPost]
        [Route("register/")]
        // GET: Register User
        public ActionResult Register([Bind(Include = "Username,Email,Password,ConfirmPassword")] RegUserBm bind)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie != null &&
                AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            var passwordMatch = bind.Password == bind.ConfirmPassword;
            if (this.ModelState.IsValid &&
                passwordMatch)
            {
                this.service.AddUser(bind);
                return this.RedirectToAction("Login");
            }

            return this.RedirectToAction("Register");
        }

        [HttpGet]
        [Route("login/")]
        public ActionResult Login()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie != null &&
                AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            this.GetUsernameOfLoggedUser();

            return this.View();
        }

        [HttpPost]
        [Route("login/")]
        public ActionResult Login([Bind(Include = "Username,Password")] LogUserBm bind)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie != null &&
                AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            var validModelState = this.ModelState.IsValid;
            var userExists = this.service.UserExists(bind);
            if (validModelState &&
                userExists)
            {
                var sessionId = Session.SessionID;
                var cookie = new HttpCookie("sessionId", sessionId);

                this.service.LoginUser(bind, sessionId);
                this.Response.SetCookie(cookie);
                ViewBag.Username = bind.Username;

                return this.RedirectToAction("All", "Cars");
            }

            return this.RedirectToAction("Login");
        }

        [HttpPost]
        [Route("logout/")]
        public ActionResult Logout()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null ||
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login");
            }

            var sessionId = Request.Cookies.Get("sessionId").Value;
            AuthenticationManager.Logout(sessionId);

            return this.RedirectToAction("All", "Cars");
        }
    }
}