using CapstoneSkinMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneSkinMarket.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            db.Games.ToList();
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
