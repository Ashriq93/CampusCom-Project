using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using System.Web.UI.WebControls.WebParts;
using CampusCom.Data;
using CampusCom.Models;
using static System.Collections.Specialized.BitVector32;
using System.Security.Policy;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Web.Services.Description;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace CampusCom.Controllers
{
    public class UserController : Controller
    {
        private CampusComContext db = new CampusComContext();

        ////////////////////////public CampusComContext Db { get => db; set => db = value; }

        // GET: User
        public ActionResult Index()
        {
            // Check if the user is authenticated
            if (Session["UserID"] != null)
            {
                var users = db.USERs.ToList();

                int userId = (int)Session["UserID"];

                // Retrieve the user from the database based on the UserID
                var user = db.USERs.Find(userId);

                if (user != null && user.userRole == "Admin")
                {
                    // User is an admin; redirect to the admin view
                    return RedirectToAction("Index", "Admin");
                }

                return RedirectToAction("Index", "Home", users);
                /*return View(users);*/
            }
            else
            {
                // User is not authenticated; redirect them to the login page
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult users()
        {
            var users = db.USERs.ToList();
            return View(users);
        }

        public ActionResult CreateAdmin()
        {
            // Your logic for creating an admin goes here
            return View();
        }


        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,FirstName,LastName,DoB,Email,sNum,Contact,userRole,Passwd,Img")] USER uSER, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    using (BinaryReader reader = new BinaryReader(imageFile.InputStream))
                    {
                        byte[] imageData = reader.ReadBytes(imageFile.ContentLength);
                        uSER.Img = imageData; // Assign the image data directly to the Img property
                    }

                    db.USERs.Add(uSER);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }

            return View(uSER);
        }



        public FileContentResult GetImage(int id)
        {
            USER user = db.USERs.Find(id);

            if (user != null && user.Img != null)
            {
                return File(user.Img, "image/jpeg"); // Adjust the content type based on your image type (e.g., image/jpeg, image/png).
            }
            else
            {
                // Return a default image or placeholder image if the user has no image.
                byte[] defaultImage = System.IO.File.ReadAllBytes(Server.MapPath("~/Uploads/UserDefaultPic.jpg"));

                return File(defaultImage, "image/jpeg");
            }
        }





        //GET: User/Edit/5


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "UserID,UserName,FirstName,LastName,DoB,Email,sNum,Contact,userRole,Passwd,Img")] USER uSER, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the user from the database


                // Check if a new profile picture has been uploaded
                if (imageFile != null)
                {
                    using (BinaryReader reader = new BinaryReader(imageFile.InputStream))
                    {
                        byte[] imageData = reader.ReadBytes(imageFile.ContentLength);
                        uSER.Img = imageData; // Assign the image data directly to the Img property
                    }
                }

                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(uSER);
        }




        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USER uSER = db.USERs.Find(id);
            db.USERs.Remove(uSER);
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
        
        public ActionResult Login()
        {
            LoginModel model = new LoginModel(); // Create a LoginModel object
            return View(model);
        }

        public class LoginViewModel
        {
            public USER User { get; set; }
            public LoginModel LoginModel { get; set; }
        }

        public class LoginModel
        {
            [Required]
            [Display(Name = "Username or Email")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Passwd { get; set; }
        }

        public ActionResult Logout()
        {
            // Remove the user's session to log them out
            Session.Remove("UserID");

            // Redirect the user to the login page or another appropriate location
            return RedirectToAction("Login", "User");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to authenticate the user based on the provided username and password
                var user = db.USERs.FirstOrDefault(u => u.UserName == model.Username && u.Passwd == model.Passwd);

                if (user != null)
                {
                    // Authentication successful; set the user's authentication status in the session
                    Session["UserID"] = user.UserID;

                    // Redirect the user to their home page
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    // Authentication failed; add a model error to display a message to the user
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            // If authentication fails or the model is invalid, return the login view with errors
            return View(model);
        }




    }
}
