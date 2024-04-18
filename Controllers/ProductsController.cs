using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapstoneSkinMarket.Models;

namespace CapstoneSkinMarket.Controllers
{
    public class ProductsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Games);
            return View(products.ToList());
        }

        public ActionResult Search(string searchTerm)
        {
            var products = db.Products.Where(p => p.Nome.Contains(searchTerm)).ToList();
            return View("Index", products);
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.GiocoID = new SelectList(db.Games, "GiocoID", "NomeGioco");
            return View();
        }

        // POST: Products/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ArticoloID,Nome,Immagine,Descrizione,Prezzo,Rarita,GiocoID")] Products products, HttpPostedFileBase Immagine)
        {
            if (ModelState.IsValid)
            {
                if (Immagine != null && Immagine.ContentLength > 0)
                {
                    // Definisce il nuovo percorso per salvare il file
                    var fileName = Path.GetFileName(Immagine.FileName);
                    var path = Path.Combine(Server.MapPath("~/Stile/Img"), fileName);  // Modifica qui il percorso di salvataggio

                    // Crea la directory se non esiste
                    var directory = Path.GetDirectoryName(path);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    Immagine.SaveAs(path);
                    products.Immagine = fileName; // Salva solo il nome del file nel database
                }

                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Se il ModelState non è valido, ricarica la vista con le informazioni correnti
            ViewBag.GiocoID = new SelectList(db.Games, "GiocoID", "NomeGioco", products.GiocoID);
            return View(products);
        }


        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.GiocoID = new SelectList(db.Games, "GiocoID", "NomeGioco", products.GiocoID);
            return View(products);
        }

        // POST: Products/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ArticoloID,Nome,Immagine,Descrizione,Prezzo,Rarita,GiocoID")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GiocoID = new SelectList(db.Games, "GiocoID", "NomeGioco", products.GiocoID);
            return View(products);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
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
