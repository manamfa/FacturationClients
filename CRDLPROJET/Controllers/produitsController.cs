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
    public class produitsController : Controller
    {
        private facturationclientBOUEntities4 db = new facturationclientBOUEntities4();

        // GET: produits
        public ActionResult Index()
        {
            var produits = db.produits.Include(p => p.categorie).Include(p => p.fournisseur);
            return View(produits.ToList());
        }
        public ActionResult Index1()
        {
            var produits = db.produits.Include(p => p.categorie).Include(p => p.fournisseur);
            return View(produits.ToList());
        }

        // GET: produits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produit produit = db.produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }
        // GET: produits/Details/5
        
        public ActionResult Details1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produit produit = db.produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

        // GET: produits/Create
        public ActionResult Create()
        {
            var fournisseur = db.fournisseurs;
            ViewData["fournisseur"] = fournisseur.ToList();

            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie");
            ViewBag.fournisseurID = new SelectList(db.fournisseurs, "fournisseurID", "nomfournisseur");
            return View();
        }
        // POST: produits/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "produitID,categorieID,fournisseurID,nomproduit,prixdeventeunitaire,Quantite,prixunitaireachat,dateentree")] produit produit)
        {
            if (ModelState.IsValid)
            {
                db.produits.Add(produit);
                db.SaveChanges();

                TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                return RedirectToAction("Index");
            }

            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie", produit.categorieID);
            ViewBag.fournisseurID = new SelectList(db.fournisseurs, "fournisseurID", "nomfournisseur", produit.fournisseurID);
            return View(produit);
        }
        // GET: produits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produit produit = db.produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie", produit.categorieID);
            ViewBag.fournisseurID = new SelectList(db.fournisseurs, "fournisseurID", "nomfournisseur", produit.fournisseurID);
            return View(produit);
        }

        // POST: produits/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "produitID,categorieID,fournisseurID,nomproduit,prixdeventeunitaire,Quantite,prixunitaireachat,dateentree")] produit produit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produit).State = EntityState.Modified;
                db.SaveChanges();

                TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                return RedirectToAction("Index", "produits");
            }
            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie", produit.categorieID);
            ViewBag.fournisseurID = new SelectList(db.fournisseurs, "fournisseurID", "nomfournisseur", produit.fournisseurID);
            return View(produit);
        }

        // GET: produits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produit produit = db.produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }
        // POST: produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            produit produit = db.produits.Find(id);
            db.produits.Remove(produit);
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



