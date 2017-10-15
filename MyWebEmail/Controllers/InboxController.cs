using BLL;
using MyWebEmail.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MyWebEmail.Controllers
{
    public class InboxController : Controller
    {
        private MailService _mailService;
        public InboxController()
        {
            _mailService = new MailService();
        }

        public ActionResult Index()
        {
            ViewBag.Message = new UserService().GetUser();
            
            List<Mail> sentMailList = _mailService.GetMails(Stauts.Received);
            return View(sentMailList);
        }
    }
}