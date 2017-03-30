using System.Web.Mvc;
using CarDealer.Models.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CarDealer.Services;

namespace CarDealerApp.Controllers
{
    public class SalesController : Controller
    {
        private SalesService service;

        public SalesController()
        {
            this.service = new SalesService();
        }

        [HttpGet]
        public ActionResult All()
        {
            IEnumerable<SaleVm> vms = this.service.GetAllSales();        
            return this.View(vms);
        }

        [HttpGet]
        public ActionResult About(int id)
        {
            SaleVm saleVm = this.service.GetSale(id);

            return this.View(saleVm);
        }

        [HttpGet]
        public ActionResult Discounted(double? percent)
        {
            IEnumerable<SaleVm> sales = this.service.GetDiscountedSales(percent);
            return this.View(sales);
        }



    }
}
