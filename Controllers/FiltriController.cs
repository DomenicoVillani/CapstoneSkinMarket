using CapstoneSkinMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneSkinMarket.Controllers
{
    public class FiltriController : Controller
    {
        DBContext db = new DBContext();
        // GET: Filtri
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenoDieci()
        {
            var prodotti = db.Products.Where(o => o.Prezzo < 10).ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult DieciVenticinque()
        {
            var prodotti = db.Products.Where(o => o.Prezzo > 10 && o.Prezzo<25).ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult VenticinqueSettantacinque()
        {
            var prodotti = db.Products.Where(o => o.Prezzo > 25 && o.Prezzo < 75).ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult SettantacinqueCentocinquanta()
        {
            var prodotti = db.Products.Where(o => o.Prezzo > 75 && o.Prezzo < 150).ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult CentocinquantaCinquecento()
        {
            var prodotti = db.Products.Where(o => o.Prezzo > 150 && o.Prezzo < 500).ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult CinquecentoPiu()
        {
            var prodotti = db.Products.Where(o => o.Prezzo > 500).ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult DefaultRare()
        {
            var prodotti = db.Products.Where(o => o.Rarita == "Default").ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult Rara()
        {
            var prodotti = db.Products.Where(o => o.Rarita == "Rara").ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult Ultra()
        {
            var prodotti = db.Products.Where(o => o.Rarita == "Ultra").ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }


        public ActionResult UltraRara()
        {
            var prodotti = db.Products.Where(o => o.Rarita == "Ultra rara").ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult RaraSegreta()
        {
            var prodotti = db.Products.Where(o => o.Rarita == "Rara Segreta").ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult Leggendaria()
        {
            var prodotti = db.Products.Where(o => o.Rarita == "Leggendaria").ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult Cs()
        {
            var prodotti = db.Products.Where(o => o.Games.GiocoID == 1).ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult Fortnite()
        {
            var prodotti = db.Products.Where(o => o.Games.GiocoID == 3).ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }
         
        public ActionResult Tf()
        {
            var prodotti = db.Products.Where(o => o.Games.GiocoID == 2).ToList();
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

    }
}