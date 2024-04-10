using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapstoneSkinMarket.Models;
using Newtonsoft.Json;

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
            var ordini = db.OrdersProducts
                .Include(o => o.Orders)
                .Include(o => o.Orders.Users)
                .Include(o => o.Products)
                .Where(o => o.OrdineID == id).ToList();
            if (ordini == null)
            {
                return HttpNotFound();
            }
            return View(ordini);
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
            var ordine = db.OrdersProducts
                .Where(o => o.OrdineID == ordersProducts.OrdineID)
                .Where(o => o.ArticoloID == ordersProducts.ArticoloID)
                .FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (ordine == null)
                {
                    db.OrdersProducts.Add(ordersProducts);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ordine.Quantita += ordersProducts.Quantita;
                    db.Entry(ordine).State = EntityState.Modified;
                }

            }

            ViewBag.OrdineID = new SelectList(db.Orders, "OrdineID", "Note", ordersProducts.OrdineID);
            ViewBag.ArticoloID = new SelectList(db.Products, "ArticoloID", "Nome", ordersProducts.ArticoloID);
            return View(ordersProducts);
        }
        //carrello cookie
        [HttpPost]
        public ActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prodotto = db.Products.FirstOrDefault(p => p.ArticoloID == id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            List<Cart> carts = new List<Cart>();
            // Creazione del cookie
            HttpCookie cartCookie;

            // Verifica se il cookie "Cart" esiste già
            if (Request.Cookies["Cart" + User.Identity.Name] != null && Request.Cookies["Cart" + User.Identity.Name]["User"] != null)
            {
                // Se esiste, aggiorna direttamente il valore
                cartCookie = Request.Cookies["Cart" + User.Identity.Name];
                // Decodifica il valore del cookie e riempie la lista
                var cartJson = HttpUtility.UrlDecode(Request.Cookies["Cart" + User.Identity.Name]["User"]);
                carts = JsonConvert.DeserializeObject<List<Cart>>(cartJson);
            }
            else
            {
                // Altrimenti, crea un nuovo cookie
                cartCookie = new HttpCookie("Cart" + User.Identity.Name);
                cartCookie.Values["User"] = User.Identity.Name;
            }
            // Verifica se l'articolo è già presente nel carrello
            var existingItem = carts.FirstOrDefault(item => item.Products.ArticoloID == prodotto.ArticoloID);
            if (existingItem != null)
            {
                // Se l'articolo è già presente, incrementa la quantità
                existingItem.Quantita++;
            }
            else
            {
                // Aggiungi l'articolo al carrello
                var cart = new Cart()
                {
                    Products = new Products()
                    {
                        ArticoloID = prodotto.ArticoloID,
                        Nome = prodotto.Nome,
                        Immagine = prodotto.Immagine,
                        Descrizione = prodotto.Descrizione,
                        Prezzo = prodotto.Prezzo,
                        Rarita = prodotto.Rarita,
                        GiocoID = prodotto.GiocoID,
                    },
                    Quantita = 1,
                    UserID = Convert.ToInt32(User.Identity.Name),
                };
                carts.Add(cart);
            }
            // Serializza la lista e aggiorna il valore del cookie
            cartCookie["User"] = HttpUtility.UrlEncode(JsonConvert.SerializeObject(carts));
            cartCookie.Expires = DateTime.Now.AddDays(1);
            // Aggiunge o aggiorna il cookie nella risposta
            Response.Cookies.Add(cartCookie);

            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public ActionResult Cart()
        {
            List<Cart> cart = new List<Cart>();
            // Verifica se il cookie "Cart" esiste già
            if (Request.Cookies["Cart" + User.Identity.Name] != null && Request.Cookies["Cart" + User.Identity.Name]["User"] != null)
            {
                var cartJson = HttpUtility.UrlDecode(Request.Cookies["Cart" + User.Identity.Name]["User"]);
                var userId = Convert.ToInt32(User.Identity.Name);

                // Decodifica il valore del cookie e riempie la lista
                var carts = JsonConvert.DeserializeObject<List<Cart>>(cartJson);

                // Filtra solo gli articoli relativi all'utente attuale
                cart = carts.Where(a => a.UserID == userId).ToList();
                ViewBag.UserCart = cart;
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrderFromCart(Orders ordini)
        {
            if (ModelState.IsValid)
            {
                ordini.UserID = Convert.ToInt32(User.Identity.Name);
                var cartJson = HttpUtility.UrlDecode(Request.Cookies["Cart" + User.Identity.Name]["User"]);
                var userId = Convert.ToInt32(User.Identity.Name);
                var articoliCart = JsonConvert.DeserializeObject<List<Cart>>(cartJson);
                var userCart = articoliCart.Where(a => a.UserID == userId).ToList();

                decimal tot = 0;
                foreach (var art in userCart)
                {
                    tot += (art.Quantita * art.Products.Prezzo);
                }
                ordini.TotalePrezzo = tot;
                ordini.DataOrdine = DateTime.Today;

                db.Orders.Add(ordini);
                db.SaveChanges();

                int newOrdineId = ordini.OrdineID;

                foreach (var art in userCart)
                {
                    var newOrdArt = new OrdersProducts();
                    newOrdArt.ArticoloID = art.Products.ArticoloID;
                    newOrdArt.OrdineID = newOrdineId;
                    newOrdArt.Quantita = Convert.ToInt32(art.Quantita);
                    db.OrdersProducts.Add(newOrdArt);
                }
                db.SaveChanges();

                HttpCookie userCookie = Request.Cookies["Cart" + User.Identity.Name];
                if (userCookie != null)
                {
                    userCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(userCookie);
                }
                return RedirectToAction("Details", "OrdersProducts", new { id = newOrdineId });
            }
            else
            {
                //se il modello non e' valido visualizza gli errori di validazione
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    System.Diagnostics.Debug.WriteLine(modelError.ErrorMessage);
                }
                return RedirectToAction("Cart");
            }
        }

        [HttpPost]
        public ActionResult SvuotaCarrello()
        {
            // Rimuovi il cookie del carrello per l'utente corrente
            HttpCookie userCookie = Request.Cookies["Cart" + User.Identity.Name];
            if (userCookie != null)
            {
                userCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(userCookie);
            }

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public ActionResult RimuoviDallCarrello(int articoloId)
        {
            // Recupera l'elenco degli articoli attualmente nel carrello dal cookie
            var artsCart = new List<Cart>();
            if (Request.Cookies["Cart" + User.Identity.Name] != null && Request.Cookies["Cart" + User.Identity.Name]["User"] != null)
            {
                var cartJson = HttpUtility.UrlDecode(Request.Cookies["Cart" + User.Identity.Name]["User"]);
                artsCart = JsonConvert.DeserializeObject<List<Cart>>(cartJson);
            }
            // Trova l'articolo con l'ID specificato nell'elenco e rimuovilo
            var artRemove = artsCart.FirstOrDefault(a => a.Products.ArticoloID == articoloId);
            if (artRemove != null)
            {
                artsCart.Remove(artRemove);
            }
            // Aggiorna il cookie del carrello con l'elenco aggiornato degli articoli
            var cartCookie = new HttpCookie("Cart" + User.Identity.Name);
            if (artsCart.Any()) // Verifica se ci sono ancora articoli nel carrello
            {
                cartCookie.Values["User"] = HttpUtility.UrlEncode(JsonConvert.SerializeObject(artsCart));
                cartCookie.Expires = DateTime.Now.AddDays(1);
            }
            else
            {
                // Se il carrello è vuoto, elimina completamente il cookie
                cartCookie.Expires = DateTime.Now.AddDays(-1);
            }
            Response.Cookies.Add(cartCookie);

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public ActionResult AggiornaQuantita(int id, string operazione)
        {
            // Recupera l'elenco degli articoli attualmente nel carrello dal cookie
            var artsCart = new List<Cart>();
            if (Request.Cookies["Cart" + User.Identity.Name] != null && Request.Cookies["Cart" + User.Identity.Name]["User"] != null)
            {
                var cartJson = HttpUtility.UrlDecode(Request.Cookies["Cart" + User.Identity.Name]["User"]);
                artsCart = JsonConvert.DeserializeObject<List<Cart>>(cartJson);
            }
            // Trova l'articolo con l'ID specificato nell'elenco
            var art = artsCart.FirstOrDefault(a => a.Products.ArticoloID == id);
            if (art != null)
            {
                // Aggiorna la quantità in base all'operazione
                if (operazione == "incrementa")
                {
                    art.Quantita++;
                }
                else if(operazione == "decrementa" && art.Quantita > 1)
                {
                    art.Quantita--;
                }
            }
            // Aggiorna il cookie del carrello con l'elenco aggiornato degli articoli
            var cartCookie = new HttpCookie("Cart" + User.Identity.Name);
            cartCookie.Values["User"] = HttpUtility.UrlEncode(JsonConvert.SerializeObject(artsCart));
            cartCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cartCookie);

            return RedirectToAction("Cart");
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
