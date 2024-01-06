using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CampusCom.Data;
using CampusCom.Models;

namespace CampusCom.Controllers
{
    public class CommunityController : Controller
    {
        private CampusComContext db = new CampusComContext();

        // GET: Community
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                // User is not authenticated; redirect them to the login page
                return RedirectToAction("Login", "User");
            }
            return View(db.COMMUNITies.ToList());
        }

        //The Community
        public ActionResult Community()
        {
            return View();
        }

        // GET: Community/Details/5
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

        public ActionResult JoinCommunity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Get the community by its ID
            var community = db.COMMUNITies.Find(id);

            if (community == null)
            {
                return HttpNotFound();
            }

            // Assuming you have user authentication implemented, get the currently logged-in user
            /*var user = *//* Get the currently logged-in user *//*;*/

            var currentUserId = (int)Session["UserID"];
            // Check if the user is already a member of the community
            /*bool isMember = db.MEMBERLISTs.Any(m => m.ComId == id && m.UserId == user.UserID);*/

            bool isMember = db.MEMBERLISTs.Any(m => m.ComId == id && m.UserId == currentUserId);

            if (isMember)
            {
                // The user is already a member, handle accordingly (e.g., show a message).
            }
            else
            {
                // Add the user to the community's member list
                var newMember = new MEMBERLIST
                {
                    UserId = currentUserId,
                    ComId = community.ComId,
                    isOwner = false // Set to false assuming the user is not an owner
                };

                db.MEMBERLISTs.Add(newMember);
                db.SaveChanges();

                // Redirect or show a success message to the user.
            }

            return RedirectToAction("Details", new { id = id });
        }


        // GET: Community/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Community/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComId,UserId,ComName,ComDescr,Category,Privacy,CreatedDate,ComStatus,ComImg")] COMMUNITY cOMMUNITY, HttpPostedFileBase ComImg)
        {
            if (ModelState.IsValid)
            {
                if (ComImg != null && ComImg.ContentLength > 0)
                {
                    using (BinaryReader reader = new BinaryReader(ComImg.InputStream))
                    {
                        byte[] imageData = reader.ReadBytes(ComImg.ContentLength);
                        cOMMUNITY.ComImg = imageData;
                    }
                }

                db.COMMUNITies.Add(cOMMUNITY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOMMUNITY);
        }

        // GET: Community/Edit/5
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

        // POST: Community/Edit/5
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

        // GET: Community/Delete/5
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

        // POST: Community/Delete/5
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
