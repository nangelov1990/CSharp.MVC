
namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CarDealer.Models.BindingModels;
    using CarDealer.Models.ViewModels;
    using CarDealer.Services;

    [RoutePrefix("parts")]
    public class PartsController : Controller
    {
        private PartsService service;

        public PartsController()
        {
            this.service = new PartsService();
        }

        [HttpGet]
        [Route]
        // GET: All parts
        public ActionResult All()
        {
            IEnumerable<AllPartVm> viewModels =
                this.service.AllParts();

            return this.View(viewModels);
        }

        [HttpGet]
        [Route("add")]
        // GET: Add Part
        public ActionResult Add()
        {
            IEnumerable<AddPartSupplierVm> viewModels =
                this.service.LoadSuppliers();

            return View(viewModels);
        }

        [HttpPost]
        [Route("add")]
        // POST: Add Part
        public ActionResult Add([Bind(Include = "Name,Price,Quantity,SupplierId")] PartBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddPart(bind);

                return this.Redirect("/parts");
            }

            return this.View();
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        // GET: Edit Part
        public ActionResult Edit(int id)
        {
            EditPartVm viewModel =
                this.service.ViewEditPart(id);

            return this.View(viewModel);
        }
    }
}