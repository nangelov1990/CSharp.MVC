namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using CarDealer.Models.BindingModels.Suppliers;
    using Security;
    using CarDealer.Models.ViewModels.Suppliers;
    using CarDealer.Services;

    [RoutePrefix("suppliers")]
    public class SuppliersController : CarDealerController
    {
        private new SuppliersService service;

        public SuppliersController()
        {
            this.service = new SuppliersService();
        }

        [HttpGet]
        [Route("{type:regex(local|importers)}/")]
        // GET: Suppliers
        public ActionResult All(string type)
        {
            IEnumerable<AllSuppliersVm> viewModels =
                this.service.GetAllSuppliersByType(type);

            this.GetUsernameOfLoggedUser();
            TempData["Supplier Type"] = char.ToUpper(type[0]) + type.Substring(1);

            return View(viewModels);
        }

        [HttpGet]
        [Route("add/")]
        // GET: Add Supplier
        public ActionResult Add()
        {
            // Only logged user can add supplier
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie == null ||
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            this.GetUsernameOfLoggedUser();

            return this.View();
        }

        [HttpPost]
        [Route("add/")]
        // POST: Add Supplier
        public ActionResult Add([Bind(Include = "Name,Type")] SupplierBm bind)
        {
            // Only logged user can add supplier
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie == null ||
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            this.GetUsernameOfLoggedUser();
            this.service.AddSupplier(bind);

            return this.RedirectToAction("All", "Cars");
        }

        [HttpGet]
        [Route("edit/{id:int}/")]
        // GET: Edit Supplier
        public ActionResult Edit(int id)
        {
            // Only logged user can add supplier
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie == null ||
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            this.GetUsernameOfLoggedUser();
            SupplierVm viewModel =
                this.service.EditModelView(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("edit/{id:int}/")]
        // GET: Edit Supplier
        public ActionResult Edit([Bind(Include = "Id,Name,Type")] SupplierBm bind)
        {
            // Only logged user can add supplier
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie == null ||
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            this.GetUsernameOfLoggedUser();
            this.service.EditModel(bind);

            return this.RedirectToAction("All", "Cars");
        }

        [HttpGet]
        [Route("delete/{id:int}/")]
        // GET: Delete Supplier
        public ActionResult Delete(int id)
        {
            // Only logged user can add supplier
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie == null ||
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            this.GetUsernameOfLoggedUser();
            SupplierVm viewModel =
                this.service.DeleteModelView(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("delete/{id:int}/")]
        // GET: Delete Supplier
        public ActionResult Delete([Bind(Include = "Id,Name,Type")] SupplierBm bind)
        {
            // Only logged user can add supplier
            var httpCookie = Request.Cookies.Get("sessionId");
            if (httpCookie == null ||
                !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            this.GetUsernameOfLoggedUser();
            this.service.DeleteModel(bind);

            return this.RedirectToAction("All", "Cars");
        }
    }
}