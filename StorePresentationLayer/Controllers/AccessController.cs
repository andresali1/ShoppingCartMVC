using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StorePresentationLayer.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Reset()
        {
            return View();
        }

        public ActionResult ChangePass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Client obj)
        {
            int result;
            string message = string.Empty;

            ViewData["C_name"] = string.IsNullOrEmpty(obj.C_name) ? "" : obj.C_name;
            ViewData["C_surname"] = string.IsNullOrEmpty(obj.C_surname) ? "" : obj.C_surname;
            ViewData["Email"] = string.IsNullOrEmpty(obj.Email) ? "" : obj.Email;

            return View();
        }
    }
}