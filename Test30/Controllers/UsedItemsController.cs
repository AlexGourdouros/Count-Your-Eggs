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
    public class UsedItemsController : Controller
    {
        private Test30Entities db = new Test30Entities();

        // GET: UsedItems
        public ActionResult Index()
        {
            String userName = Convert.ToString(Session["userName"]);
            var usedItems = db.UsedItems.Include(u => u.Item).Include(u => u.User).Where(c => c.User.UserName == userName);
            return View(usedItems.ToList());
        }
        public ActionResult Index2()
        {
            String userName = Convert.ToString(Session["userName"]);
            var usedItems = db.UsedItems.Include(u => u.Item).Include(u => u.User).Where(c => c.User.UserName == userName);
            return View(usedItems.ToList());
        }

        // GET: UsedItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedItem usedItem = db.UsedItems.Find(id);
            if (usedItem == null)
            {
                return HttpNotFound();
            }
            return View(usedItem);
        }

        // GET: UsedItems/Create
        public ActionResult Create()
        {
            String userName = Convert.ToString(Session["userName"]);
            var Foo = db.Items.ToList().Where(c => c.User.UserName == userName);
            ViewBag.ItemID = new SelectList(Foo, "ItemID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: UsedItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsedID,ItemID,Quantity,DateUsed,UserID")] UsedItem usedItem)
        {
            if (ModelState.IsValid)
            {
                db.UsedItems.Add(usedItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", usedItem.ItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", usedItem.UserID);
            return View(usedItem);
        }

        // GET: UsedItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedItem usedItem = db.UsedItems.Find(id);
            if (usedItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", usedItem.ItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", usedItem.UserID);
            return View(usedItem);
        }

        // POST: UsedItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsedID,ItemID,Quantity,DateUsed,UserID")] UsedItem usedItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usedItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", usedItem.ItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", usedItem.UserID);
            return View(usedItem);
        }

        // GET: UsedItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedItem usedItem = db.UsedItems.Find(id);
            if (usedItem == null)
            {
                return HttpNotFound();
            }
            return View(usedItem);
        }

        // POST: UsedItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsedItem usedItem = db.UsedItems.Find(id);
            db.UsedItems.Remove(usedItem);
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
