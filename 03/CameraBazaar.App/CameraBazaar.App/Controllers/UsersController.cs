using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CameraBazaar.App.Controllers
{
    using Models.BindingModels;
    using Services;

    [RoutePrefix("Users")]
    public class UsersController : Controller
    {
        private new UsersService service;

        public UsersController()
        {
            this.service = new UsersService();
        }

        [HttpGet]
        [Route("Register/")]
        // GET: Register User
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        // POST: Register User
        public ActionResult Register([Bind(Include = "username, email, pasword, phone")] UserBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddUser(bind);
            }
        }
    }
}