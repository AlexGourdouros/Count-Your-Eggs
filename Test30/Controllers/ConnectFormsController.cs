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
    public class ConnectFormsController : Controller
    {
        private Test30Entities db = new Test30Entities();

        // GET: ConnectForms
        public ActionResult Index()
        {
            var connectForms = db.ConnectForms.Include(c => c.Country);
            return View(connectForms.ToList());
        }

        // GET: ConnectForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConnectForm connectForm = db.ConnectForms.Find(id);
            if (connectForm == null)
            {
                return HttpNotFound();
            }
            return View(connectForm);
        }

        // GET: ConnectForms/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name");
            return View();
        }

        // POST: ConnectForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConnectID,FirstName,LastName,Email,PhoneNumber,RestaurantName,City,CountryID,Message")] ConnectForm connectForm)
        {
           

            if (ModelState.IsValid)
            {
                db.ConnectForms.Add(connectForm);
                db.SaveChanges();
                // return RedirectToAction("Index");
                TempData["ASAP"] = "We will get back to you ASAP";
                return RedirectToAction("Contact", "Home");
            }

            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", connectForm.CountryID);
            return View(connectForm);
        }

        // GET: ConnectForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConnectForm connectForm = db.ConnectForms.Find(id);
            if (connectForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", connectForm.CountryID);
            return View(connectForm);
        }

        // POST: ConnectForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConnectID,FirstName,LastName,Email,PhoneNumber,RestaurantName,City,CountryID,Message")] ConnectForm connectForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(connectForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", connectForm.CountryID);
            return View(connectForm);
        }

        // GET: ConnectForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConnectForm connectForm = db.ConnectForms.Find(id);
            if (connectForm == null)
            {
                return HttpNotFound();
            }
            return View(connectForm);
        }

        // POST: ConnectForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConnectForm connectForm = db.ConnectForms.Find(id);
            db.ConnectForms.Remove(connectForm);
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
