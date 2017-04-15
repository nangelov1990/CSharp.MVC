namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

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
        [Route("{type:regex(local|importers)}")]
        // GET: Suppliers
        public ActionResult All(string type)
        {
            IEnumerable<AllSuppliersVm> viewModels =
                this.service.GetAllSuppliersByType(type);

            this.GetUsernameOfLoggedUser();

            return View(viewModels);
        }
    }
}