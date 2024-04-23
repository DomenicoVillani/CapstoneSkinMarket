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
    public class UsersController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Users
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Search(string searchTerm)
        {
            var userSearch = db.Users.Where(p => p.Username.Contains(searchTerm)).ToList();
            return View("Index", userSearch);
        }
        // GET: Users/Details/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }


        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,Nome,Cognome,Email,Password,DataNascita,CodFiscale,Telefono")] Users users)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .Select(x => new { x.Key, Errors = x.Value.Errors.Select(y => y.ErrorMessage).ToArray() })
                                .ToArray();
            }

            if (ModelState.IsValid)
            {
                var userDb = db.Users.FirstOrDefault(u => u.Email == users.Email);
                if (userDb == null)
                {
                    db.Users.Add(users);
                    db.SaveChanges();
                    TempData["message"] = "Utente registrato correttamente";
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    TempData["message"] = "L'email esiste già";
                    return View(users);
                }
            }
            return View(users);
        }


        // GET: Users/Edit/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public ActionResult Edit([Bind(Include = "UserID,Username,Nome,Cognome,Email,Password,DataNascita,CodFiscale,Telefono")] Users user, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(upload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Stile/Img/Propic"), fileName);
                    upload.SaveAs(path);
                    user.ProPic = fileName; // Aggiorna il percorso dell'immagine del profilo
                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = user.UserID });
            }
            return View(user);
        }


        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
