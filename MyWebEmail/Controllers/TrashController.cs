using MyWebEmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MyWebEmail.Controllers
{
    public class TrashController : Controller
    {
        private MailService _mailService;
        public TrashController()
        {
            _mailService = new MailService();
        }

        public ActionResult Index()
        {
            List<Mail> trashMailList = _mailService.GetMails(Stauts.Trash);
            return View(trashMailList);
        }
    }
}