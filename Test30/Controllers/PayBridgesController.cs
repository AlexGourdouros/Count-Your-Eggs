using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test30.Models;

namespace Test30.Controllers
{
    public class PayBridgesController : Controller
    {
        private Test30Entities db = new Test30Entities();

        // GET: PayBridges
        public ActionResult Index()
        {
            String userName = Convert.ToString(Session["userName"]);
            var payBridges = db.PayBridges.Include(p => p.Invoice).Include(p => p.Payment1).Include(p => p.User).Where(c => c.User.UserName == userName);
            return View(payBridges.ToList());
        }

        // GET: PayBridges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayBridge payBridge = db.PayBridges.Find(id);
            if (payBridge == null)
            {
                return HttpNotFound();
            }
            return View(payBridge);
        }

        // GET: PayBridges/Create
        public ActionResult Create()
        {
            String userName = Convert.ToString(Session["userName"]);
            var InvoiceID = db.Invoices.ToList().Where(c => c.User.UserName == userName);
            ViewBag.InvoiceID = new SelectList(InvoiceID, "InvoiceID", "InvoiceID");
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: PayBridges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaybridgeID,InvoiceID,PaymentID,UserID,Payment,PaymentDate")] PayBridge payBridge)
        {
            if (ModelState.IsValid)
            {
                db.PayBridges.Add(payBridge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "InvoiceID", payBridge.InvoiceID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType", payBridge.PaymentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", payBridge.UserID);
            return View(payBridge);
        }

        // GET: PayBridges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayBridge payBridge = db.PayBridges.Find(id);
            if (payBridge == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "InvoiceID", payBridge.InvoiceID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType", payBridge.PaymentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", payBridge.UserID);
            return View(payBridge);
        }

        // POST: PayBridges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaybridgeID,InvoiceID,PaymentID,UserID,Payment,PaymentDate")] PayBridge payBridge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payBridge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "InvoiceID", payBridge.InvoiceID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType", payBridge.PaymentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", payBridge.UserID);
            return View(payBridge);
        }

        // GET: PayBridges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayBridge payBridge = db.PayBridges.Find(id);
            if (payBridge == null)
            {
                return HttpNotFound();
            }
            return View(payBridge);
        }

        // POST: PayBridges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayBridge payBridge = db.PayBridges.Find(id);
            db.PayBridges.Remove(payBridge);
            db.SaveChanges();
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
