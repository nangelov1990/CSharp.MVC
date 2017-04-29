using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CameraBazaar.App.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        // GET: Register User
        public ActionResult Register()
        {
            return View();
        }
    }
}