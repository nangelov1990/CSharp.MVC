
namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CarDealer.Models.BindingModels;
    using CarDealer.Models.BindingModels.Parts;
    using CarDealer.Models.ViewModels;
    using CarDealer.Models.ViewModels.Parts;
    using CarDealer.Models.ViewModels.Suppliers;
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
        [Route("delete/{id:int}")]
        // GET: Delete Part
        public ActionResult Delete(int id)
        {
            DeletePartVm viewModel =
                this.service.DeletePartView(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        // POST: Delete Part
        public ActionResult Delete([Bind(Include = "Id")] DeletePartBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.DeletePart(bind.Id);

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

        [HttpPost]
        [Route("edit/{id:int}")]
        // POST: Edit Part
        public ActionResult Edit([Bind(Include = "Id,Name,Price")] EditPartBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditPart(bind);

                return this.Redirect("/parts");
            }

            return this.View();
        }
    }
}