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
    public class ItemsController : Controller
    {
        private Test30Entities db = new Test30Entities();

        // GET: Items
        public ActionResult Index()
        {
            String userName = Convert.ToString(Session["userName"]);
            var items = db.Items.Include(i => i.Category).Include(i => i.User).Where(c => c.User.UserName == userName).Include(i => i.Status);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        public ActionResult _Spoilage()
        {


            using (var db=new Test30Entities() )
            {
                var Spoilages = db.Spoilages.ToList();
                return View("_Spoilage");
            }
        }
        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,Name,Quantity,UserID,Price,Total,CategoryID,statusID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", item.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", item.UserID);
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name", item.statusID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", item.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", item.UserID);
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name", item.statusID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,Name,Quantity,UserID,Price,Total,CategoryID,statusID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", item.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", item.UserID);
            ViewBag.statusID = new SelectList(db.Status, "statusID", "Name", item.statusID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           /* Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
            */

            try
            {

                Item item = db.Items.Find(id);
                db.Items.Remove(item);
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
