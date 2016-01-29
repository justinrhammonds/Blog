using Blog.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Blog.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactMessage contact)
        {
            var Emailer = new EmailService();
            var email = new IdentityMessage
            {
                Subject = "Message",
                Destination = ConfigurationManager.AppSettings["ContactEmail"],
                Body = "You have a new message from: " + contact.Name + " (" + 
                    contact.Email + ") with the following contents: \n\n" + contact.Message
            };
            Emailer.SendAsync(email);

            return RedirectToAction("index", "Post");
        }
    }
}