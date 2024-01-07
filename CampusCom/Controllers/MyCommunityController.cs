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
    public class MyCommunityController : Controller
    {
        private CampusComContext db = new CampusComContext();

        // GET: MyCommunity
        public ActionResult Index(int? id)
        {
            if (Session["UserID"] == null)
            {
                // User is not authenticated; redirect them to the login page
                return RedirectToAction("Login", "User");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userCommunities = db.COMMUNITies
           .Where(c => c.UserId == id)
           .ToList();

            return View(userCommunities);
        }

        // GET: MyCommunity/Details/5
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

            // Retrieve the list of posts for the specified community
            var posts = db.POSTs.Where(p => p.ComId == id).ToList();

            // Create a view model that includes both the community and its posts
            var viewModel = new CommunityDetailsViewModel
            {
                Community = cOMMUNITY,
                Posts = posts
            };

            return View(viewModel);
        }


        public ActionResult DetailsBroad(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            COMMUNITY community = db.COMMUNITies.Find(id);
            if (community == null)
            {
                return HttpNotFound();
            }

            // Retrieve the list of events for the specified community
            var events = db.EVENTINGs.Where(e => e.ComId == id).ToList();

            // Create a view model that includes both the community and its events
            var viewModel = new CommunityDetailsWithEventsViewModel
            {
                Community = community,
                Events = events
            };

            return View(viewModel);
        }


        public ActionResult DetailsMem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            COMMUNITY community = db.COMMUNITies.Find(id);

            if (community == null)
            {
                return HttpNotFound();
            }

            // Retrieve the list of members for the specified community
            var members = db.MEMBERLISTs.Where(m => m.ComId == id).ToList();

            // Create a view model that includes both the community and its members
            var viewModel = new CommunityMemberViewModel
            {
                Community = community,
                MemberList = members
            };

            return View(viewModel);
        }

        public ActionResult DetailsReq(int? id)
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

        public ActionResult DetailsSet(int? id)
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

        // GET: MyCommunity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyCommunity/Create
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


        // GET: MyCommunity/Edit/5
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

        // POST: MyCommunity/Edit/5
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

        // GET: MyCommunity/Delete/5
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

        // POST: MyCommunity/Delete/5
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


        public ActionResult DeleteMember(int? memberId)
        {
            if (memberId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MEMBERLIST member = db.MEMBERLISTs.Find(memberId);

            if (member == null)
            {
                return HttpNotFound();
            }

            return View(member);
        }

        [HttpPost, ActionName("DeleteMember")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMemberConfirmed(int memberId)
        {
            MEMBERLIST member = db.MEMBERLISTs.Find(memberId);

            if (member == null)
            {
                return HttpNotFound();
            }

            db.MEMBERLISTs.Remove(member);
            db.SaveChanges();

            // Redirect to the DetailsMem action to refresh the member list
            return RedirectToAction("DetailsMem", new { id = member.ComId });
        }

    }
}
