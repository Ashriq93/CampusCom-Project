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
    public class PubEventController : Controller
    {
        private CampusComContext db = new CampusComContext();

        // GET: PubEvent
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                // User is not authenticated; redirect them to the login page
                return RedirectToAction("Login", "User");
            }
            var eVENTINGs = db.EVENTINGs.Include(e => e.COMMUNITY);
            return View(eVENTINGs.ToList());
        }

        // GET: PubEvent/Details/5
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

    }
}
