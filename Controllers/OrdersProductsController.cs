using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapstoneSkinMarket.Models;

namespace CapstoneSkinMarket.Controllers
{
    public class OrdersProductsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: OrdersProducts
        public ActionResult Index()
        {
            var ordersProducts = db.OrdersProducts.Include(o => o.Orders).Include(o => o.Products);
            return View(ordersProducts.ToList());
        }

        // GET: OrdersProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersProducts ordersProducts = db.OrdersProducts.Find(id);
            if (ordersProducts == null)
            {
                return HttpNotFound();
            }
            return View(ordersProducts);
        }

        // GET: OrdersProducts/Create
        public ActionResult Create()
        {
            ViewBag.OrdineID = new SelectList(db.Orders, "OrdineID", "Note");
            ViewBag.ArticoloID = new SelectList(db.Products, "ArticoloID", "Nome");
            return View();
        }

        // POST: OrdersProducts/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticoloID,OrdineID")] OrdersProducts ordersProducts)
        {
            if (ModelState.IsValid)
            {
                db.OrdersProducts.Add(ordersProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrdineID = new SelectList(db.Orders, "OrdineID", "Note", ordersProducts.OrdineID);
            ViewBag.ArticoloID = new SelectList(db.Products, "ArticoloID", "Nome", ordersProducts.ArticoloID);
            return View(ordersProducts);
        }

        // GET: OrdersProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersProducts ordersProducts = db.OrdersProducts.Find(id);
            if (ordersProducts == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrdineID = new SelectList(db.Orders, "OrdineID", "Note", ordersProducts.OrdineID);
            ViewBag.ArticoloID = new SelectList(db.Products, "ArticoloID", "Nome", ordersProducts.ArticoloID);
            return View(ordersProducts);
        }

        // POST: OrdersProducts/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticoloID,OrdineID")] OrdersProducts ordersProducts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordersProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrdineID = new SelectList(db.Orders, "OrdineID", "Note", ordersProducts.OrdineID);
            ViewBag.ArticoloID = new SelectList(db.Products, "ArticoloID", "Nome", ordersProducts.ArticoloID);
            return View(ordersProducts);
        }

        // GET: OrdersProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersProducts ordersProducts = db.OrdersProducts.Find(id);
            if (ordersProducts == null)
            {
                return HttpNotFound();
            }
            return View(ordersProducts);
        }

        // POST: OrdersProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdersProducts ordersProducts = db.OrdersProducts.Find(id);
            db.OrdersProducts.Remove(ordersProducts);
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
