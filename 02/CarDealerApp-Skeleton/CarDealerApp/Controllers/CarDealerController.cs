namespace CarDealerApp.Controllers
{
    using System.Web.Mvc;

    using Security;
    using CarDealer.Services;

    public class CarDealerController : Controller
    {
        protected Service service;

        public CarDealerController()
        {
            this.service = new Service();
        }

        protected void GetUsernameOfLoggedUser()
        {
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie != null &&
                AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                ViewBag.Username = service.GetLoggedUsername(httpCookie.Value);
            }
        }
    }
}