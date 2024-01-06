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
    public class JoinedController : Controller
    {
        private CampusComContext db = new CampusComContext();

        // GET: Joined
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Retrieve community IDs where the user is a member
            var communityIds = db.MEMBERLISTs
                .Where(m => m.UserId == id)
                .Select(m => m.ComId)
                .ToList();

            // Retrieve communities based on the community IDs
            var userCommunities = db.COMMUNITies
                .Where(c => communityIds.Contains(c.ComId))
                .ToList();

            return View(userCommunities);
            /*return View(db.COMMUNITies.ToList());*/
        }

        // GET: Joined/Details/5
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

    }
}
