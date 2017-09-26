using MyWebEmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MyWebEmail.Controllers
{
    public class DraftController : Controller
    {
        private MailService _mailService;
        public DraftController()
        {
            _mailService = new MailService();
        }

        public ActionResult Index()
        {
            List<Mail> draftMailList = _mailService.GetMails(Stauts.Draft);
            return View(draftMailList);
        }
    }
}