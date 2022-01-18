using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRDLPROJET.Models;

namespace CRDLPROJET.Controllers
{
    public class usersController : Controller
    {
        private facturationclientBOUEntities4 db = new facturationclientBOUEntities4();


        // GET: users
        public ActionResult Index()
        {

           
            var users = db.users.Include(u => u.profil);
            return View(users.ToList());
        }
        public ActionResult Index1()
        {

            var users = db.users.Include(u => u.profil);
            return View(users.ToList());

        }
        // GET: users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user user)
        {
            var v = from p in db.profils
                    join u in db.users
                        on p.profileID equals u.userID
                    where (u.username == user.username && u.userpassword == user.userpassword)
                    select new 
                    {
                        profilename = p.profilename,
                        username = u.username,
                        email = u.useremail,
                        datein = u.dateinsertion,
                        password = u.userpassword,
                        ID = u.userID,
                        profi=u.profileID,
                    };

            var result = v.FirstOrDefault();

            if (result != null)
            {   
                Session["username"] = result.username;
                Session["email"] = result.email;
                Session["profilename"] = result.profilename;
                Session["datein"] = result.datein;
                Session["password"] = result.password;
                Session["ID"] = result.ID;
                Session["profi"] = result.profi;

                if (result.profilename == "utilisateur")
                {

                    TempData["msg"] = "<script>alert('Login successfully');</script>";

                    return RedirectToAction("Index1", "produits");
                }
                else if (result.profilename == "admin")
                {

                    TempData["msg"] = "<script>alert('Login successfully');</script>";

                    return RedirectToAction("Index", "produits");
                }
                else
                {

                    TempData["msg"] = "<script>alert('Login Failled');</script>";

                    return RedirectToAction("Login", "users");
                }
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session["username"] = null;
            Session["email"] = null;
            Session["profilename"] = null;
            Session["datein"] = null;
            Session["password"] = null;
            Session["ID"] = null;
            Session["profi"] = null;

            return RedirectToAction("Login", "users");
        }


        // GET: users/Create
        public ActionResult Create()
        {
            ViewBag.profileID = new SelectList(db.profils, "profileID", "profilename");
            return View();
        }
        // POST: users/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,profileID,username,userpassword,useremail,dateinsertion")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();

                TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                return RedirectToAction("Index");
            }

            ViewBag.profileID = new SelectList(db.profils, "profileID", "profilename", user.profileID);
            return View(user);
        }

        // GET: users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.profileID = new SelectList(db.profils, "profileID", "profilename", user.profileID);
            return View(user);
        }

        // POST: users/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,profileID,username,userpassword,useremail,dateinsertion")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                return RedirectToAction("Index");
            }
            ViewBag.profileID = new SelectList(db.profils, "profileID", "profilename", user.profileID);
            return View(user);
        }

        // GET: users/Edit/5
        public ActionResult Edit1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.profileID = new SelectList(db.profils, "profileID", "profilename", user.profileID);
            return View(user);
        }
        // POST: users/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "userID,profileID,username,userpassword,useremail,dateinsertion")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                //TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                return RedirectToAction("Index1");
            }
            ViewBag.profileID = new SelectList(db.profils, "profileID", "profilename", user.profileID);
            return View(user);
        }
        // GET: users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();

            TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

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


