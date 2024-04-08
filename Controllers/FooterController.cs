using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneSkinMarket.Controllers
{
    public class FooterController : Controller
    {
        // GET: Footer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChiSiamo() 
        {
            return View();
        }

        public ActionResult DoveTrovarci()
        {
            return View();
        }

        public ActionResult Contattaci()
        {
            return View();
        }
    }
}