using BusinessLayer;
using EntityLayer;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AdminPresentationLayer.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        // GET: ChangePass
        public ActionResult ChangePass()
        {
            return View();
        }

        public ActionResult Reset()
        {
            return View();
        }

        /// <summary>
        /// Metod to Login User
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="pass">User password</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string email, string pass)
        {
            App_User oUser = new App_User();

            oUser = new BL_User().GetUsers().Where(u => u.Email == email && u.Pass == BL_Resources.Sha256Converter(pass)).FirstOrDefault();

            if (oUser == null)
            {
                ViewBag.Error = "Correo o contraseña incorrecta";
                return View();
            }
            else
            {
                if(oUser.IsReset)
                {
                    TempData["UserId"] = oUser.UserId;

                    return RedirectToAction("ChangePass");
                }

                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult ChangePass(string userId, string currentPass, string newPass, string newPassConfirm)
        {
            App_User oUser = new App_User();

            oUser = new BL_User().GetUsers().Where(u => u.UserId == Convert.ToInt32(userId)).FirstOrDefault();

            if(oUser.Pass != BL_Resources.Sha256Converter(currentPass))
            {
                TempData["UserId"] = userId;
                ViewData["cPass"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if(newPass != newPassConfirm)
            {
                TempData["UserId"] = userId;
                ViewData["cPass"] = currentPass;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            ViewData["cPass"] = "";
            newPass = BL_Resources.Sha256Converter(newPass);

            string message = string.Empty;
            bool response = new BL_User().ChangePassword(Convert.ToInt32(userId), newPass, out message);

            if (response)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["UserId"] = userId;
                ViewBag.Error = message;
                return View();
            }
        }
    }
}