using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test30.Models;

namespace Test30.Controllers
{
    public class InvoicesController : Controller
    {
        private Test30Entities db = new Test30Entities();

        // GET: Invoices
        public ActionResult Index()
        {
           
            String userName = Convert.ToString(Session["userName"]);
            var invoices = db.Invoices.Include(i => i.Distributor).Include(i => i.User).Where(c => c.User.UserName == userName).Include(i => i.Status);
            return View(invoices.ToList());
        }
      

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceID,InvoiceCreated,TotalPaid,TotalDue,Total,DistributorId,UserID,ItemsReceived,InvoiceNumber,statusID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {

            
                db.Invoices.Add(invoice);
                invoice.UpdateTotalPaid();
                 invoice.UpdateTotalDue();
                 invoice.UpdateTotal();
             //   invoice.SetStatusID();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // String userName = Convert.ToString(Session["userName"]);

            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name", invoice.DistributorId);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoice.UserID);
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name", invoice.statusID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name", invoice.DistributorId);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoice.UserID);
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name", invoice.statusID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceID,InvoiceCreated,TotalPaid,TotalDue,Total,DistributorId,UserID,ItemsReceived,InvoiceNumber,statusID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {

             
                 
                db.Entry(invoice).State = EntityState.Modified;
                invoice.UpdateTotalPaid();
                invoice.UpdateTotalDue();
                invoice.UpdateTotal();
              //  invoice.SetStatusID();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name", invoice.DistributorId);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoice.UserID);
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name", invoice.statusID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
           
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            
            
                Invoice invoice = db.Invoices.Find(id);


                if (invoice == null)
                {
                    return HttpNotFound();
                }
            
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {






            try
            {

                Invoice invoice = db.Invoices.Find(id);


                db.Invoices.Remove(invoice);

                db.SaveChanges();
            }
            catch (SystemException)
            {
                return RedirectToAction("Index");
                throw;
            }


             return RedirectToAction("Index");

     

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
