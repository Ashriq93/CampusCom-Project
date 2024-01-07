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
    public class AdminController : Controller
    {
        private CampusComContext db = new CampusComContext();

        // GET: Admin
        public ActionResult Index()
        {
            var pendingCommunities = db.COMMUNITies
            .Where(c => c.ComStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
            .ToList();

            return View(pendingCommunities);
            /*return View(db.COMMUNITies.ToList());*/
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMUNITY cOMMUNITY = db.COMMUNITies.Find(id);
            if (cOMMUNITY == null)
            {
                return HttpNotFound();
            }
            return View(cOMMUNITY);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComId,UserId,ComName,ComDescr,Category,Privacy,CreatedDate,ComStatus,ComImg")] COMMUNITY cOMMUNITY)
        {
            if (ModelState.IsValid)
            {
                db.COMMUNITies.Add(cOMMUNITY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOMMUNITY);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMUNITY cOMMUNITY = db.COMMUNITies.Find(id);
            if (cOMMUNITY == null)
            {
                return HttpNotFound();
            }
            return View(cOMMUNITY);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComId,UserId,ComName,ComDescr,Category,Privacy,CreatedDate,ComStatus,ComImg")] COMMUNITY cOMMUNITY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMMUNITY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOMMUNITY);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMUNITY cOMMUNITY = db.COMMUNITies.Find(id);
            if (cOMMUNITY == null)
            {
                return HttpNotFound();
            }
            return View(cOMMUNITY);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COMMUNITY cOMMUNITY = db.COMMUNITies.Find(id);
            db.COMMUNITies.Remove(cOMMUNITY);
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
