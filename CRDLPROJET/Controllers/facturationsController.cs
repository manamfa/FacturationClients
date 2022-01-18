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
    public class facturationsController : Controller
    {
        private facturationclientBOUEntities4 db = new facturationclientBOUEntities4();

        // GET: facturations
        public ActionResult Index()
        {
            var facturations = db.facturations.Include(f => f.client);
            return View(facturations.ToList());
        }
        public ActionResult Index1()
        {
            var facturations = db.facturations.Include(f => f.client);
            return View(facturations.ToList());
        }
        // GET: facturations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facturation facturation = db.facturations.Find(id);
            if (facturation == null)
            {
                return HttpNotFound();
            }
            return View(facturation);
        }

        // GET: facturations/Create
        public ActionResult Create()
        {
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient");
            return View();
        }
        // POST: facturations/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "facturationID,clientID,datefacturation")] facturation facturation)
        {
            if (ModelState.IsValid)
            {
                db.facturations.Add(facturation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", facturation.clientID);
            return View(facturation);
        }
        // GET: facturations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facturation facturation = db.facturations.Find(id);
            if (facturation == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", facturation.clientID);
            return View(facturation);
        }
        // POST: facturations/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "facturationID,clientID,datefacturation")] facturation facturation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", facturation.clientID);
            return View(facturation);
        }

        // GET: facturations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facturation facturation = db.facturations.Find(id);
            if (facturation == null)
            {
                return HttpNotFound();
            }
            return View(facturation);
        }
        // POST: facturations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            facturation facturation = db.facturations.Find(id);
            db.facturations.Remove(facturation);
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

