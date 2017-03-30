using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CarDealer.Models.ViewModels;
using CarDealer.Services;

namespace CarDealerApp.Controllers
{
    public class SuppliersController : Controller
    {
        private SuppliersService service;

        public SuppliersController()
        {
            this.service = new SuppliersService();
        }

        [HttpGet]
        public ActionResult All(string type)
        {
            IEnumerable<SupplierVm> viewModels = this.service.GetAllSuppliersByType(type);
            return this.View(viewModels);
        }
    }
}
