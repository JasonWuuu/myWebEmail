using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MyWebEmail.Models;

namespace MyWebEmail.Controllers
{
    public class SettingController : Controller
    {
        
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Save(Setting setting) {
            Session["UserSetting"] = setting;
            return RedirectToAction("Index", "Inbox");
        }
    }
}