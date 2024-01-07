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
    public class EventController : Controller
    {
        private CampusComContext db = new CampusComContext();

        // GET: Event
        public ActionResult Index()
        {
            var eVENTINGs = db.EVENTINGs.Include(e => e.COMMUNITY);
            return View(eVENTINGs.ToList());
        }

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTING eVENTING = db.EVENTINGs.Find(id);
            if (eVENTING == null)
            {
                return HttpNotFound();
            }
            return View(eVENTING);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            ViewBag.ComId = new SelectList(db.COMMUNITies, "ComId", "ComName");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,ComId,Title,EventDate,EventDescr,Venue,Privacy")] EVENTING eVENTING)
        {
            if (ModelState.IsValid)
            {
                db.EVENTINGs.Add(eVENTING);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComId = new SelectList(db.COMMUNITies, "ComId", "ComName", eVENTING.ComId);
            return View(eVENTING);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTING eVENTING = db.EVENTINGs.Find(id);
            if (eVENTING == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComId = new SelectList(db.COMMUNITies, "ComId", "ComName", eVENTING.ComId);
            return View(eVENTING);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,ComId,Title,EventDate,EventDescr,Venue,Privacy")] EVENTING eVENTING)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eVENTING).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComId = new SelectList(db.COMMUNITies, "ComId", "ComName", eVENTING.ComId);
            return View(eVENTING);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTING eVENTING = db.EVENTINGs.Find(id);
            if (eVENTING == null)
            {
                return HttpNotFound();
            }
            return View(eVENTING);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EVENTING eVENTING = db.EVENTINGs.Find(id);
            db.EVENTINGs.Remove(eVENTING);
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
