using CapstoneSkinMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
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
        public ActionResult EmailMandata()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(EmailElastic email)
        {
            try
            {
                string smtpServer = "smtp.elasticemail.com";
                int smtpPort = 2525;
                string smtpUser = "skinbazaarclient@gmail.com";
                string smtpPassword = "5F412D24268B10746B6918CC1CCEAB5BDF58";


                using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                    smtpClient.EnableSsl = true;


                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpUser),
                        Subject = email.Oggetto,
                        Body = $"Email mandata da "+ email.EmailMittente +"\n"+ email.ContenutoEmail,
                        IsBodyHtml = false,
                    };

                    mailMessage.To.Add(smtpUser);

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Errore: " + ex.Message;
            }
            return View("EmailMandata");
        }
    }
}