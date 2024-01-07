using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CampusCom.Data;
using CampusCom.Models;

namespace CampusCom.Controllers
{
    public class MembershipController : Controller
    {
        private CampusComContext db = new CampusComContext();

        // GET: Membership
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                // User is not authenticated; redirect them to the login page
                return RedirectToAction("Login", "User");
            }
            var mEMBERLISTs = db.MEMBERLISTs.Include(m => m.COMMUNITY).Include(m => m.USER);
            return View(mEMBERLISTs.ToList());
        }

        // GET: Membership/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBERLIST mEMBERLIST = db.MEMBERLISTs.Find(id);
            if (mEMBERLIST == null)
            {
                return HttpNotFound();
            }
            return View(mEMBERLIST);
        }

        // GET: Membership/Create
        public ActionResult Create()
        {
            ViewBag.ComId = new SelectList(db.COMMUNITies, "ComId", "ComName");
            ViewBag.UserId = new SelectList(db.USERs, "UserID", "UserName");
            return View();
        }

        // POST: Membership/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemId,UserId,ComId,isOwner")] MEMBERLIST mEMBERLIST)
        {
            if (ModelState.IsValid)
            {
                db.MEMBERLISTs.Add(mEMBERLIST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComId = new SelectList(db.COMMUNITies, "ComId", "ComName", mEMBERLIST.ComId);
            ViewBag.UserId = new SelectList(db.USERs, "UserID", "UserName", mEMBERLIST.UserId);
            return View(mEMBERLIST);
        }

        // GET: Membership/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBERLIST mEMBERLIST = db.MEMBERLISTs.Find(id);
            if (mEMBERLIST == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComId = new SelectList(db.COMMUNITies, "ComId", "ComName", mEMBERLIST.ComId);
            ViewBag.UserId = new SelectList(db.USERs, "UserID", "UserName", mEMBERLIST.UserId);
            return View(mEMBERLIST);
        }

        // POST: Membership/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemId,UserId,ComId,isOwner")] MEMBERLIST mEMBERLIST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mEMBERLIST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComId = new SelectList(db.COMMUNITies, "ComId", "ComName", mEMBERLIST.ComId);
            ViewBag.UserId = new SelectList(db.USERs, "UserID", "UserName", mEMBERLIST.UserId);
            return View(mEMBERLIST);
        }

        // GET: Membership/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBERLIST mEMBERLIST = db.MEMBERLISTs.Find(id);
            if (mEMBERLIST == null)
            {
                return HttpNotFound();
            }
            return View(mEMBERLIST);
        }

        // POST: Membership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MEMBERLIST mEMBERLIST = db.MEMBERLISTs.Find(id);
            db.MEMBERLISTs.Remove(mEMBERLIST);
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
