﻿using System.Web.Mvc;
using PersonalSite.MVC.UI.Models;
using System.Configuration;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Web;
using System.Net;


namespace PersonalSite.MVC.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            //ViewBag.Section = About();

            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }

        public ActionResult Projects()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        { 

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                string body = $"{cvm.Name} has sent you the following message:<br/>" +
                    $"{cvm.Message} <strong>from the email address:<strong> {cvm.Email}.";

                MailMessage mm = new MailMessage(
                    ConfigurationManager.AppSettings["EmailUser"].ToString(),
                    ConfigurationManager.AppSettings["EmailTo"].ToString(),
                    cvm.Subject,
                    body);

                mm.IsBodyHtml = true;
                mm.Priority = MailPriority.High;
                mm.ReplyToList.Add(cvm.Email);

                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailPass"].ToString());

                try
                {
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    ViewBag.CustomerMessage =
                        $"We're sorry your request could not be completed at this time." +
                        $" Please try again later. Error Message: <br /> {ex.StackTrace}";
                    return View(cvm);
                    
                }
                return View("EmailConfirmation", cvm);
            }

            return View(cvm);
        }
    }
}
