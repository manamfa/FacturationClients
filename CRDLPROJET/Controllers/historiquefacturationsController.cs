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
    public class historiquefacturationsController : Controller
    {
        private facturationclientBOUEntities4 db = new facturationclientBOUEntities4();

        // GET: historiquefacturations
        public ActionResult Index()
        {
            
            var historiquefacturations = db.historiquefacturations.Include(h => h.client).Include(h => h.client1).Include(h => h.client2).Include(h => h.client3).Include(h => h.facturation).Include(h => h.facturation1).Include(h => h.facturation2).Include(h => h.facturation3).Include(h => h.produit).Include(h => h.produit1).Include(h => h.produit2).Include(h => h.produit3);
            return View(historiquefacturations.ToList());

        }

        [HttpPost]
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.sms = "Historique des clients en Date du" + startDate + "Au" + endDate;
            var historiquefacturations = db.historiquefacturations.Where(x => x.datehistofacturation >= startDate && x.datehistofacturation <= endDate).ToList();
            return View(historiquefacturations);
        }
        public ActionResult Index2()
        {
            var historiquefacturations = db.historiquefacturations.Include(h => h.client).Include(h => h.client1).Include(h => h.client2).Include(h => h.client3).Include(h => h.facturation).Include(h => h.facturation1).Include(h => h.facturation2).Include(h => h.facturation3).Include(h => h.produit).Include(h => h.produit1).Include(h => h.produit2).Include(h => h.produit3);
            return View(historiquefacturations.ToList());
        }

        [HttpPost]
        public ActionResult Index2(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.sms = "Historique des clients en Date du"+startDate+"Au"+ endDate;

            var historiquefacturations = db.historiquefacturations.Where(x => x.datehistofacturation >= startDate && x.datehistofacturation <= endDate).ToList();
            return View(historiquefacturations);
        }

        public ActionResult Index1()
        {
           List<Histofactmodel> model = (from d in db.historiquefacturations 
                                         
                            select new { d.datehistofacturation, d.Total,d.facturationID} into x
                            group x by new { x.datehistofacturation } into g
                            select new Histofactmodel
                            {
                                datefac = (g.Key.datehistofacturation).Value,
                                Sommeto = (g.Select(x => x.Total).Sum()).Value,
                                factura = (g.Select(x => x.facturationID).Count()),

                            }).ToList();
            return View(model);

        }
        public ActionResult Index3()
        {
            List<Histofactmodel> model = (from d in db.historiquefacturations

                                          select new { d.datehistofacturation, d.Total, d.facturationID } into x
                                          group x by new { x.datehistofacturation } into g
                                          select new Histofactmodel
                                          {
                                              datefac = (g.Key.datehistofacturation).Value,
                                              Sommeto = (g.Select(x => x.Total).Sum()).Value,
                                              factura = (g.Select(x => x.facturationID).Count()),
                                          }).ToList();
            return View(model);

        }
        public ActionResult Index4()
        {
            List<Histofactmodel> model = (from d in db.historiquefacturations 
                                          //join c in db.clients on d.facturationID equals c.clientID

                                          select new { d.datehistofacturation, d.Total, d.facturationID,d.clientID} into x
                                          group x by new { x.facturationID } into g
                                          select new Histofactmodel
                                          {
                                              factura = (g.Key.facturationID).Value,
                                              Sommeto = (g.Select(x => x.Total).Sum()).Value,
                                              
                                          }).ToList();
            return View(model);
        }
        public ActionResult Index5()
        {
            List<Histofactmodel> model = (from d in db.historiquefacturations join p in db.produits
                on d.produitID equals p.produitID 
            //join c in db.clients on d.facturationID equals c.clientID


            select new { d.datehistofacturation, d.Quantite,d.Total,d.produitID, p.nomproduit} into x
            group x by new { x.datehistofacturation,x.nomproduit,x.produitID} into g
            select new Histofactmodel
            {
                datefac = (g.Key.datehistofacturation).Value,
                Quant = (g.Select(x => x.Quantite).Sum()).Value,
                Sommeto = (g.Select(x => x.Total).Sum()).Value,
                nomp = g.Key.nomproduit,
            }).ToList();
         return View(model);

        }       
        public ActionResult Quantitev()
        {
            List<Histofactmodel> model = (from d in db.historiquefacturations join p in db.produits
                on d.produitID equals p.produitID 
            //join c in db.clients on d.facturationID equals c.clientID


            select new { d.datehistofacturation, d.Quantite,d.Total,d.produitID, p.nomproduit} into x
            group x by new { x.datehistofacturation,x.nomproduit,x.produitID} into g
            select new Histofactmodel
            {
                datefac = (g.Key.datehistofacturation).Value,
                Quant = (g.Select(x => x.Quantite).Sum()).Value,
                Sommeto = (g.Select(x => x.Total).Sum()).Value,
                nomp = g.Key.nomproduit,
            }).ToList();
         return View(model);

        }
         public ActionResult Index6()
        {
            List<Histofactmodel> model = (from d in db.historiquefacturations join p in db.produits
                on d.produitID equals p.produitID 
            join c in db.clients on d.clientID equals c.clientID


            select new { d.datehistofacturation, d.Quantite, d.produitID, p.nomproduit,c.nomclient,c.prenomclient,c.clientID} into x
            group x by new { x.datehistofacturation,x.nomproduit,x.produitID,x.clientID,x.nomclient,x.prenomclient} into g
            select new Histofactmodel
            {
                datefac = (g.Key.datehistofacturation).Value,
                Quant = (g.Select(x => x.Quantite).Sum()).Value,
                //nomp = g.Select(x => x.nomclient),   

            }).ToList();
         return View(model);

        }
        //public ActionResult Index5()
        //{
        //      List<Histofactmodel> model = (from C in entites.Clients
        //                                       join P in entites.PanneVehicules on C.Id equals P.ClientsId
        //                                       join R in entites.Reparations on P.Id equals R.PanneVehiculesId
        //                                       where P.EtatVehicule.Equals("Terminer")
        //                                       select new ReparationModel
        //                                       {
        //                                           Nom = C.NomClient,
        //                                           Prenom = C.Prenomclient,
        //                                           Telephone = C.Telephone,
        //                                           Plaque = P.Plaque,
        //                                           Marque = P.Marque,
        //                                           Modele = P.Modele,
        //                                           MontantTotal = (R.MontantTotal).Value,
        //                                           DateReparation = (R.DateReparation).Value,
        //                                           EtatReparation = R.EtatReparation,
        //                                           EtatVehicule = P.EtatVehicule,
        //                                           Quantite = (R.Quantite).Value,
        //                                           PanneId = C.Id
        //                                       }).ToList();
        //        return View(model);

        //}



        //public ActionResult Index1(historiquefacturation ltm)
        //{

        //    facturationclientBOUEntities db = new facturationclientBOUEntities();
        //    var historiquefacturation = db.historiquefacturations;
        //    ViewData["historiquefacturation"] = historiquefacturation.ToList();

        //    string query = "select datehistofacturation,sum(Total) As 'Somme' from historiquefacturation group by datehistofacturation";

        //    var data = db.Database.SqlQuery<historiquefacturation>(query).ToList();

        //    return View(data);
        //}

        // GET: historiquefacturations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiquefacturation historiquefacturation = db.historiquefacturations.Find(id);
            if (historiquefacturation == null)
            {
                return HttpNotFound();
            }
            return View(historiquefacturation);
        }

        // GET: historiquefacturations/Create
        public ActionResult Create()
        {
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient","prenomclient","numtel","adresse");
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", "prenomclient", "numtel", "adresse");
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", "prenomclient", "numtel", "adresse");
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", "prenomclient", "numtel", "adresse");
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", "datefacturation");
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", "datefacturation");
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", "datefacturation");
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", "datefacturation");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            return View();
        }

        // POST: historiquefacturations/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
      /*  [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(historiquefacturation historiquefacturation)
        {
            var v = from p in db.historiquefacturations
                        //join u in db.tableachats
                        //   on p.produitID equals u.produitID
                    where (p.datehistofacturation == historiquefacturation.datehistofacturation)
                    select new testquantite
                    {
                        quanti = p.Quantite.Value,
                        quantit = tableachat.Quantite.Value,
                    };
            if (ModelState.IsValid)
            {
                db.historiquefacturations.Add(historiquefacturation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            return View(historiquefacturation);
        }*/
        
        // GET: historiquefacturations/Edit/5
        public ActionResult Edit1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiquefacturation historiquefacturation = db.historiquefacturations.Find(id);
            if (historiquefacturation == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            return View(historiquefacturation);
        }
        // GET: historiquefacturations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiquefacturation historiquefacturation = db.historiquefacturations.Find(id);
            if (historiquefacturation == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            return View(historiquefacturation);
        }

        // POST: historiquefacturations/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "historiquefacturationID,produitID,clientID,facturationID,prixunitaire,Quantite,Total,datehistofacturation")] historiquefacturation historiquefacturation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historiquefacturation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            return View(historiquefacturation);
        }
        // POST: historiquefacturations/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "historiquefacturationID,produitID,clientID,facturationID,prixunitaire,Quantite,Total,datehistofacturation")] historiquefacturation historiquefacturation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historiquefacturation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", historiquefacturation.clientID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.facturationID = new SelectList(db.facturations, "facturationID", "facturationID", historiquefacturation.facturationID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", historiquefacturation.produitID);
            return View(historiquefacturation);
        }

        // GET: historiquefacturations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiquefacturation historiquefacturation = db.historiquefacturations.Find(id);
            if (historiquefacturation == null)
            {
                return HttpNotFound();
            }
            return View(historiquefacturation);
        }
        public ActionResult Delete1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiquefacturation historiquefacturation = db.historiquefacturations.Find(id);
            if (historiquefacturation == null)
            {
                return HttpNotFound();
            }
            return View(historiquefacturation);
        }

        // POST: historiquefacturations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            historiquefacturation historiquefacturation = db.historiquefacturations.Find(id);
            db.historiquefacturations.Remove(historiquefacturation);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
        // POST: historiquefacturations/Delete/5
        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(int id)
        {
            historiquefacturation historiquefacturation = db.historiquefacturations.Find(id);
            db.historiquefacturations.Remove(historiquefacturation);
            db.SaveChanges();
            return RedirectToAction("Index2");
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



