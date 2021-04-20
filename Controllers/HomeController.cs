using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ingredient_inator.Models;

using System.Net;
using System.Net.Mail;

namespace Ingredient_inator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Contact Record)
        {
            // We receive this
            using (MailMessage Mail = new MailMessage(Record.Email, "contact.scitechdev@gmail.com"))
            {
                Mail.Subject = "Ingredient-inator Re: Inquiry";

                Mail.Body = "From: " + Record.SenderName + " " + Record.Email + "<br/>" +
                            "Contact Number: " + Record.ContactNo + "<br/>" +
                            "Subject: <strong>" + Record.Subject + "</strong><br/>" +
                            "Message: <br/><br/><strong>" + Record.Message + "</strong><br/><br/>";
                Mail.IsBodyHtml = true;

                using (SmtpClient SMTP = new SmtpClient())
                {
                    SMTP.Host = "smtp.gmail.com";
                    SMTP.EnableSsl = true;
                    NetworkCredential NetworkCred =
                        new NetworkCredential("contact.scitechdev@gmail.com", "csb-is-2019");
                    SMTP.UseDefaultCredentials = true;
                    SMTP.Credentials = NetworkCred;
                    SMTP.Port = 587;
                    SMTP.Send(Mail);
                    ViewBag.Message = "Message sent.";
                }
            }

            // We send this
            using (MailMessage Mail = new MailMessage("contact.scitechdev@gmail.com", Record.Email))
            {
                Mail.Subject = "Hello from Ingredient-inator!";

                Mail.Body = "Hi " + Record.SenderName + ",<br/><br/>" +
                    "Thank you for reaching out to us! This is to confirm that we've " +
                    "received your message and are working on our reply.<br/><br/>" +
                    "Here are the details of your inquiry: <br/><br/>" +
                    "Subject: <strong>" + Record.Subject + "</strong><br/>" +
                    "Message: <br/><br/><strong>" + Record.Message + "</strong><br/><br/>" +
                    "Please wait for our reply. Thank you!<br/><br/>" +
                    "Sincerely, <br/><br/>" +
                    "The Ingredient-inator Team<br/><br/>" +
                    "<em>This is an automated message. Do not reply to this email.</em>" ;
                Mail.IsBodyHtml = true;

                using (SmtpClient SMTP = new SmtpClient())
                {
                    SMTP.Host = "smtp.gmail.com";
                    SMTP.EnableSsl = true;
                    NetworkCredential NetworkCred =
                        new NetworkCredential("contact.scitechdev@gmail.com", "csb-is-2019");
                    SMTP.UseDefaultCredentials = true;
                    SMTP.Credentials = NetworkCred;
                    SMTP.Port = 587;
                    SMTP.Send(Mail);
                }
            }

            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
