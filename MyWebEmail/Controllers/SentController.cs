using MyWebEmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MyWebEmail.Controllers
{
    public class SentController : Controller
    {
        private MailService _mailService;
        public SentController()
        {
            _mailService = new MailService();
        }

        public ActionResult Index()
        {
            List<Mail> sentMailList = _mailService.GetMails(Stauts.Sent);
            return View(sentMailList);
        }
    }
}