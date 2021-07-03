﻿using System;
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
    public class ReceiptsController : Controller
    {
        private Test30Entities db = new Test30Entities();

        // GET: Receipts
        public ActionResult Index()
        {
            String userName = Convert.ToString(Session["userName"]);
            var receipts = db.Receipts.Include(r => r.InvoiceItem).Include(r => r.User).Where(c => c.User.UserName == userName);
            return View(receipts.ToList());
        }

        // GET: Receipts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = db.Receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(receipt);
        }

        // GET: Receipts/Create
        public ActionResult Create()
        {
            String userName = Convert.ToString(Session["userName"]);
            var InvoiceItemID = db.InvoiceItems.ToList().Where(c => c.User.UserName == userName);
            ViewBag.InvoiceItemID = new SelectList(InvoiceItemID, "InvoiceItemID", "InvoiceItemID");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReceiptID,Quantity,ReceiptDate,InvoiceItemID,UserID")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                db.Receipts.Add(receipt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceItemID = new SelectList(db.InvoiceItems, "InvoiceItemID", "InvoiceItemID", receipt.InvoiceItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", receipt.UserID);
            return View(receipt);
        }

        // GET: Receipts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = db.Receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceItemID = new SelectList(db.InvoiceItems, "InvoiceItemID", "InvoiceItemID", receipt.InvoiceItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", receipt.UserID);
            return View(receipt);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReceiptID,Quantity,ReceiptDate,InvoiceItemID,UserID")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receipt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceItemID = new SelectList(db.InvoiceItems, "InvoiceItemID", "InvoiceItemID", receipt.InvoiceItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", receipt.UserID);
            return View(receipt);
        }

        // GET: Receipts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = db.Receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receipt receipt = db.Receipts.Find(id);
            db.Receipts.Remove(receipt);
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
