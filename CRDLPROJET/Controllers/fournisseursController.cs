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
    public class fournisseursController : Controller
    {
        private facturationclientBOUEntities4 db = new facturationclientBOUEntities4();

        // GET: fournisseurs
        public ActionResult Index()
        {
            return View(db.fournisseurs.ToList());
        }


        // GET: fournisseurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fournisseur fournisseur = db.fournisseurs.Find(id);
            if (fournisseur == null)
            {
                return HttpNotFound();
            }
            return View(fournisseur);
        }

        // GET: fournisseurs/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: fournisseurs/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fournisseurID,nomfournisseur,prenomfournisseur,tel,adresse,datent")] fournisseur fournisseur)
        {
            if (ModelState.IsValid)
            {
                db.fournisseurs.Add(fournisseur);
                db.SaveChanges();

                TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                return RedirectToAction("Index");
            }

            return View(fournisseur);
        }

        // GET: fournisseurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fournisseur fournisseur = db.fournisseurs.Find(id);
            if (fournisseur == null)
            {
                return HttpNotFound();
            }
            return View(fournisseur);
        }

        // POST: fournisseurs/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fournisseurID,nomfournisseur,prenomfournisseur,tel,adresse,datent")] fournisseur fournisseur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fournisseur).State = EntityState.Modified;
                db.SaveChanges();

                TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                return RedirectToAction("Index");
            }
            return View(fournisseur);
        }
        // GET: fournisseurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fournisseur fournisseur = db.fournisseurs.Find(id);
            if (fournisseur == null)
            {
                return HttpNotFound();
            }
            return View(fournisseur);
        }
        // POST: fournisseurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fournisseur fournisseur = db.fournisseurs.Find(id);
            db.fournisseurs.Remove(fournisseur);
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

