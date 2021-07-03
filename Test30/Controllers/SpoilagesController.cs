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
    public class SpoilagesController : Controller
    {
        private Test30Entities db = new Test30Entities();

        // GET: Spoilages
        public ActionResult Index()
        {
            String userName = Convert.ToString(Session["userName"]);
            var spoilages = db.Spoilages.Include(s => s.Item).Include(s => s.User).Where(c => c.User.UserName == userName);
            return View(spoilages.ToList());
        }

        public ActionResult Index2()
        {
            String userName = Convert.ToString(Session["userName"]);
            var spoilages = db.Spoilages.Include(s => s.Item).Include(s => s.User).Where(c => c.User.UserName == userName);
            return View(spoilages.ToList());
        }

        // GET: Spoilages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spoilage spoilage = db.Spoilages.Find(id);
            if (spoilage == null)
            {
                return HttpNotFound();
            }
            return View(spoilage);
        }

        // GET: Spoilages/Create
        public ActionResult Create()
        {
            String userName = Convert.ToString(Session["userName"]);
            var Foo = db.Items.ToList().Where(c => c.User.UserName == userName);
            ViewBag.ItemID = new SelectList(Foo, "ItemID", "Name");
           
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Spoilages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpoilageID,DateSpoiled,Reason,Quantity,ItemID,UserID")] Spoilage spoilage)
        {
            if (ModelState.IsValid)
            {
                db.Spoilages.Add(spoilage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", spoilage.ItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", spoilage.UserID);
            return View(spoilage);
        }

        // GET: Spoilages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spoilage spoilage = db.Spoilages.Find(id);
            if (spoilage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", spoilage.ItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", spoilage.UserID);
            return View(spoilage);
        }

        // POST: Spoilages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpoilageID,DateSpoiled,Reason,Quantity,ItemID,UserID")] Spoilage spoilage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spoilage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", spoilage.ItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", spoilage.UserID);
            return View(spoilage);
        }

        // GET: Spoilages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spoilage spoilage = db.Spoilages.Find(id);
            if (spoilage == null)
            {
                return HttpNotFound();
            }
            return View(spoilage);
        }

        // POST: Spoilages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spoilage spoilage = db.Spoilages.Find(id);
            db.Spoilages.Remove(spoilage);
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
