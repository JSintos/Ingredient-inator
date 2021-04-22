using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ingredient_inator.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Ingredient_inator.Data;

using System.Net;
using System.Net.Mail;

namespace Ingredient_inator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<HomeController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            Random random = new Random();

            var Recipes = _context.Recipes.ToList().OrderBy(x => random.Next()).Take(5);

            return View(Recipes);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Contact(Contact record)
        {

            var NewMessage = new Contact()
            {
                MessageId = record.MessageId,
                SenderName = record.SenderName,
                Email = record.Email,
                ContactNo = record.ContactNo,
                Subject = record.Subject,
                Message = record.Message
            };

            _context.Contact.Add(NewMessage);
            _context.SaveChanges();

            // we receive this
            using (MailMessage mail = new MailMessage(record.Email, "contact.scitechdev@gmail.com"))
            {
                mail.Subject = "Ingredient-inator Re: Inquiry";

                mail.Body = "From: " + record.SenderName + " " + record.Email + "<br/>" +
                    "Contact Number: " + record.ContactNo + "<br/>" +
                    "Subject: <strong>" + record.Subject + "</strong><br/>" +
                    "Message:<br/><strong>" + record.Message + "</strong><br/><br/>";
                mail.IsBodyHtml = true;

                using (SmtpClient SMTP = new SmtpClient())
                {
                    SMTP.Host = "smtp.gmail.com";
                    SMTP.EnableSsl = true;
                    NetworkCredential NetworkCred =
                        new NetworkCredential("contact.scitechdev@gmail.com", "csb-is-2019");
                    SMTP.UseDefaultCredentials = true;
                    SMTP.Credentials = NetworkCred;
                    SMTP.Port = 587;
                    SMTP.Send(mail);
                    ViewBag.Message = "Message sent.";
                }

            }

            // we send this
            using (MailMessage mail = new MailMessage("contact.scitechdev@gmail.com", record.Email))
            {
                mail.Subject = "Hello from Ingredient-inator!";

                mail.Body = "Hi " + record.SenderName + ",<br/><br/>" +
                    "Thank you for reaching out to us! This is to confirm that we've " +
                    "received your message and are working on our reply.<br/>" +
                    "Here are the details of your inquiry: <br/><br/>" +
                    "Subject: <strong>" + record.Subject + "</strong><br/>" +
                    "Message:<br/><strong>" + record.Message + "</strong><br/><br/>" +
                    "Please wait for our reply. Thank you!<br/><br/>" +
                    "Sincerely,<br/>" +
                    "The Ingredient-inator Team<br/><br/>" +
                    "P.S. This is an automated message." ;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred =
                        new NetworkCredential("contact.scitechdev@gmail.com", "csb-is-2019");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    ViewBag.Message = "Message sent.";
                }
            }

            return View();
        }

        [Authorize]
        public ActionResult RecipeEntities()
        {
            var userId = _userManager.GetUserId(User);

            var FoundRecipes = _context.Recipes.Where(R => R.Author == userId).ToList();
            var FoundCategories = _context.Categories.Where(C => C.Author == userId).ToList();

            RecipeCategoryViewModel RCVM = new RecipeCategoryViewModel();
            RCVM.Recipes = FoundRecipes;
            RCVM.Categories = FoundCategories;

            return View(RCVM);
        }

        [Authorize]
        public ActionResult ReviewEntities()
        {
            var userId = _userManager.GetUserId(User);

            var FoundReviews = _context.Reviews.Where(R => R.Author == userId).ToList();

            return View(FoundReviews);
        }
    }
}
