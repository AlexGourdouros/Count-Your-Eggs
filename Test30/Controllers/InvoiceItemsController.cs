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
    public class InvoiceItemsController : Controller
    {
        private Test30Entities db = new Test30Entities();

        // GET: InvoiceItems
        public ActionResult Index()
        {
            String userName = Convert.ToString(Session["userName"]);
            var invoiceItems = db.InvoiceItems.Include(i => i.Invoice).Where(c => c.User.UserName == userName).Include(i => i.Item).Include(i => i.User).Where(c => c.User.UserName == userName).Include(i => i.Status);
            return View(invoiceItems.ToList());
        }
        public ActionResult Index2()
        {
            String userName = Convert.ToString(Session["userName"]);
            var invoiceItems = db.InvoiceItems.Include(i => i.Invoice).Where(c => c.User.UserName == userName).Include(i => i.Item).Include(i => i.User).Where(c => c.User.UserName == userName).Include(i => i.Status);
            return View(invoiceItems.ToList());
        }

        // GET: InvoiceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Create
        public ActionResult Create()
        {
            String userName = Convert.ToString(Session["userName"]);
            var InvoiceID = db.Invoices.ToList().Where(c => c.User.UserName == userName);
            ViewBag.InvoiceID = new SelectList(InvoiceID, "InvoiceID", "InvoiceID");
            var Foo = db.Items.ToList().Where(c => c.User.UserName == userName);
            ViewBag.ItemID = new SelectList(Foo, "ItemID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name");
            return View();
        }

        // POST: InvoiceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceItemID,InvoiceID,ItemID,Quantity,Cost,ExtendedCost,UserID,ExpirationDate,DateAdded,statusID")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceItems.Add(invoiceItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "InvoiceID", invoiceItem.InvoiceID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", invoiceItem.ItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoiceItem.UserID);
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name", invoiceItem.statusID);
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            String userName = Convert.ToString(Session["userName"]);
            var InvoiceID = db.Invoices.ToList().Where(c => c.User.UserName == userName);
            ViewBag.InvoiceID = new SelectList(InvoiceID, "InvoiceID", "InvoiceID");
            var Foo = db.Items.ToList().Where(c => c.User.UserName == userName);
            ViewBag.ItemID = new SelectList(Foo, "ItemID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoiceItem.UserID);
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name", invoiceItem.statusID);
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceItemID,InvoiceID,ItemID,Quantity,Cost,ExtendedCost,UserID,ExpirationDate,DateAdded,statusID")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "InvoiceID", invoiceItem.InvoiceID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", invoiceItem.ItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoiceItem.UserID);
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name", invoiceItem.statusID);
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           /* InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            db.InvoiceItems.Remove(invoiceItem);
            db.SaveChanges();
            return RedirectToAction("Index");*/

            try
            {

                InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
                db.InvoiceItems.Remove(invoiceItem);
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
