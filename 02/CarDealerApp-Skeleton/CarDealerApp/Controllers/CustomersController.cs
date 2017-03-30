using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using CarDealer.Models;
using CarDealer.Data;

namespace CarDealerApp.Controllers
{
    public class CustomersController : Controller
    {
        private CarDealerContext context = new CarDealerContext();

        // GET: Ordered Customers
        public ActionResult All(string parameter)
        {
            IList<Customer> orderedCmrs;
            string listOrder = null;
            if (parameter != null)
            {
                listOrder = parameter.ToLower();
            }

            if (listOrder == "ascending")
            {
                orderedCmrs = context.Customers
                    .OrderByDescending(c => c.BirthDate)
                    .ThenBy(c => c.IsYoungDriver)
                    .ToList();
            }
            else if (listOrder == "descending")
            {
                orderedCmrs = context.Customers
                    .OrderBy(c => c.BirthDate)
                    .ThenBy(c => c.IsYoungDriver)
                    .ToList();
            }
            else
            {
                orderedCmrs = context.Customers.ToList();
            }

            return View(orderedCmrs);
        }

        // GET: Customers
        public ActionResult Index()
        {
            var allCmrs = context.Customers.ToList();

            return View(allCmrs);
        }

        // GET: Customers/5
        public ActionResult TotalSales(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var customers = context.Customers;
            var sales = context.Sales;

            var data = customers
                .GroupJoin(sales,
                    c => c.Id,
                    s => s.Customer.Id,
                    (c, result) => new {c, result});

            return View(data);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? parameter)
        {
            if (parameter == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = context.Customers.Find(parameter);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,BirthDate,IsYoungDriver")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Customers.Add(customer);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,BirthDate,IsYoungDriver")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Entry(customer).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = context.Customers.Find(id);
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
