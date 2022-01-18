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
    public class clientsController : Controller
    {
        private facturationclientBOUEntities4 db = new facturationclientBOUEntities4();

        // GET: clients
        public ActionResult Index()
        {
            return View(db.clients.ToList());
        }

        public ActionResult Index1()
        {
            return View(db.clients.ToList());
        }
        // GET: clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        public ActionResult Details1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        // GET: clients/Create
        public ActionResult Create()
        {
            var client = db.clients;
            ViewData["client"] = client.ToList();

            var facturation = db.facturations;
            ViewData["facturation"] = facturation.ToList();

            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            return View();
        }
        public ActionResult Create1()
        {
            var client = db.clients;
            ViewData["client"] = client.ToList();

            var facturation = db.facturations;
            ViewData["facturation"] = facturation.ToList();

            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");

            return View();
        }

        // POST: clients/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(client client, tableachat tableachat, facturation facturation, historiquefacturation historiquefacturation,produit produit)
        {
            if (ModelState.IsValid)
            {
                var v = from p in db.produits
                            //join u in db.tableachats
                            //   on p.produitID equals u.produitID
                        where (p.produitID == tableachat.produitID)
                        select new testquantite
                        {
                            quanti = p.Quantite.Value,
                            quantit = tableachat.Quantite.Value,
                        };

                int k = 0; int l = 0; int rest = 0;
                foreach (var item in v)
                {
                    k = item.quanti;
                    l = item.quantit;
                }

                if (k >= l)
                {
                    rest = k - l;



                    produit produtData = db.produits.Where(u => u.produitID == produit.produitID).SingleOrDefault();
                    produtData.Quantite = rest;

                    db.clients.Add(client);
                    db.tableachats.Add(tableachat);
                    db.facturations.Add(facturation);
                    db.historiquefacturations.Add(historiquefacturation);
                    db.SaveChanges();

                    TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('La Quantite Demandée est Inférieur à Celle du Stock');</script>";
                }
                return RedirectToAction("create2", "tableachats");
            }

            return View(client);
        }
        // POST: clients/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(client client, tableachat tableachat, facturation facturation, historiquefacturation historiquefacturation,produit produit)
        {
            if (ModelState.IsValid)
            {
                var v = from p in db.produits
                            //join u in db.tableachats
                            //   on p.produitID equals u.produitID
                        where (p.produitID == tableachat.produitID)
                        select new testquantite
                        {
                            quanti = p.Quantite.Value,
                            quantit = tableachat.Quantite.Value,
                        };



                int k = 0; int l = 0; int rest = 0;
                foreach (var item in v)
                {
                    k = item.quanti;
                    l = item.quantit;
                }

                if (k >= l)
                {
                    rest = k - l;

                    produit produtData = db.produits.Where(u => u.produitID == produit.produitID).SingleOrDefault();
                    produtData.Quantite = rest;

                    db.clients.Add(client);
                    db.tableachats.Add(tableachat);
                    db.facturations.Add(facturation);
                    db.historiquefacturations.Add(historiquefacturation);
                    db.SaveChanges();

                    TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('La Quantite Demandée est Inférieur à Celle du Stock');</script>";
                }
                return RedirectToAction("createAd", "tableachats");
            }



            return View(client);
        }

        // GET: clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        public ActionResult Edit1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        // POST: clients/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clientID,nomclient,prenomclient,numtel,adresse")] client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();

                //TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                return RedirectToAction("Index");
            }
            return View(client);
        }

        // POST: clients/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "clientID,nomclient,prenomclient,numtel,adresse")] client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();

                //TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                return RedirectToAction("Index1");
            }
            return View(client);
        }
        // GET: clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            client client = db.clients.Find(id);
            db.clients.Remove(client);
            db.SaveChanges();

            TempData["msg"] = "<script>alert('La Supression Réussie avec success');</script>";

            return RedirectToAction("Index");
        }
        // GET: clients/Delete/5
        public ActionResult Delete1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: clients/Delete/5
        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(int id)
        {
            client client = db.clients.Find(id);
            db.clients.Remove(client);
            db.SaveChanges();

            TempData["msg"] = "<script>alert('La Supression Réussie avec success');</script>";
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

