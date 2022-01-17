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
    public class historiqueprixesController : Controller
    {
        private facturationclientBOUEntities4 db = new facturationclientBOUEntities4();

        // GET: historiqueprixes
        public ActionResult Index()
        {
            var historiqueprixes = db.historiqueprixes.Include(h => h.produit);
            return View(historiqueprixes.ToList());
        }
        public ActionResult Index1()
        {
            var historiqueprixes = db.historiqueprixes.Include(h => h.produit);
            return View(historiqueprixes.ToList());
        }

        // GET: historiqueprixes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiqueprix historiqueprix = db.historiqueprixes.Find(id);
            if (historiqueprix == null)
            {
                return HttpNotFound();
            }
            return View(historiqueprix);
        }
        // GET: historiqueprixes/Details/5
        public ActionResult Details1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiqueprix historiqueprix = db.historiqueprixes.Find(id);
            if (historiqueprix == null)
            {
                return HttpNotFound();
            }
            return View(historiqueprix);
        }
        // GET: historiqueprixes/Create
        public ActionResult Create()
        {
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            return View();
        }
        // POST: historiqueprixes/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "historiqueprixID,produitID,prixdeventeunitaire,datemodification")] historiqueprix historiqueprix)
        {
            if (ModelState.IsValid)
            {
                db.historiqueprixes.Add(historiqueprix);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiqueprix.produitID);
            return View(historiqueprix);
        }
        // GET: historiqueprixes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiqueprix historiqueprix = db.historiqueprixes.Find(id);
            if (historiqueprix == null)
            {
                return HttpNotFound();
            }
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiqueprix.produitID);
            return View(historiqueprix);
        }

        // POST: historiqueprixes/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "historiqueprixID,produitID,prixdeventeunitaire,datemodification")] historiqueprix historiqueprix)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historiqueprix).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiqueprix.produitID);
            return View(historiqueprix);
        }

        // GET: historiqueprixes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiqueprix historiqueprix = db.historiqueprixes.Find(id);
            if (historiqueprix == null)
            {
                return HttpNotFound();
            }
            return View(historiqueprix);
        }

        // POST: historiqueprixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            historiqueprix historiqueprix = db.historiqueprixes.Find(id);
            db.historiqueprixes.Remove(historiqueprix);
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



