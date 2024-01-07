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
    public class AcademicController : Controller
    {
        private CampusComContext db = new CampusComContext();

        // GET: Academic
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                // User is not authenticated; redirect them to the login page
                return RedirectToAction("Login", "User");
            }
            // Filter communities by the "Academic" category
            var academicCommunities = db.COMMUNITies.Where(c => c.Category == "Academic").ToList();

            return View(academicCommunities);
        }

        // GET: Academic/Details/5
        public ActionResult Details(int? id)
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
            COMMUNITY cOMMUNITY = db.COMMUNITies.Find(id);
            if (cOMMUNITY == null)
            {
                return HttpNotFound();
            }
            return View(cOMMUNITY);
        }


    }
}
