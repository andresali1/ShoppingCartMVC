using BusinessLayer;
using EntityLayer;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace StorePresentationLayer.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // GET: Reset
        public ActionResult Reset()
        {
            return View();
        }

        // GET: ChangePass
        public ActionResult ChangePass()
        {
            return View();
        }

        /// <summary>
        /// Method to register a new Client
        /// </summary>
        /// <param name="obj">Client type object with data</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(Client obj)
        {
            int result;
            string message = string.Empty;

            ViewData["C_name"] = string.IsNullOrEmpty(obj.C_name) ? "" : obj.C_name;
            ViewData["C_surname"] = string.IsNullOrEmpty(obj.C_surname) ? "" : obj.C_surname;
            ViewData["Email"] = string.IsNullOrEmpty(obj.Email) ? "" : obj.Email;

            if(obj.Pass != obj.ConfirmPass)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            result = new BL_Client().RegisterClient(obj, out message);

            if(result > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = message;
                return View();
            }
        }

        /// <summary>
        /// Method to login CLient
        /// </summary>
        /// <param name="email">Client's Email</param>
        /// <param name="pass">Client's password</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string email, string pass)
        {
            Client oClient = null;

            oClient = new BL_Client().GetClients().Where(c => c.Email == email && c.Pass == BL_Resources.Sha256Converter(pass)).FirstOrDefault();

            if(oClient == null)
            {
                ViewBag.Error = "Correo o Contraseña incorrectos";
                return View();
            }
            else
            {
                if (oClient.IsReset)
                {
                    TempData["ClientId"] = oClient.ClientId;
                    return RedirectToAction("ChangePass");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(oClient.Email, false);
                    oClient.Pass = "";
                    Session["Client"] = oClient;
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Store");
                }
            }
        }

        /// <summary>
        /// Method to restore client's forgotten password
        /// </summary>
        /// <param name="email">Client's email</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Reset(string email)
        {
            Client oClient = new Client();

            oClient = new BL_Client().GetClients().Where(c => c.Email == email).FirstOrDefault();

            if (oClient == null)
            {
                ViewBag.Error = "No se encontró un cliente relacionado a ese correo";
                return View();
            }

            string message = string.Empty;
            bool response = new BL_Client().ResetPassword(oClient.ClientId, email, out message);

            if (response)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = message;
                return View();
            }
        }

        /// <summary>
        /// Method to change Client's pass
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <param name="currentPass">Client's current pass</param>
        /// <param name="newPass">given new pass</param>
        /// <param name="newPassConfirm">confirmation of new pass</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePass(string clientId, string currentPass, string newPass, string newPassConfirm)
        {
            Client oClient = new Client();

            oClient = new BL_Client().GetClients().Where(c => c.ClientId == Convert.ToInt32(clientId)).FirstOrDefault();

            if (oClient.Pass != BL_Resources.Sha256Converter(currentPass))
            {
                TempData["ClientId"] = clientId;
                ViewData["cPass"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (newPass != newPassConfirm)
            {
                TempData["ClientId"] = clientId;
                ViewData["cPass"] = currentPass;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            ViewData["cPass"] = "";
            newPass = BL_Resources.Sha256Converter(newPass);

            string message = string.Empty;
            bool response = new BL_Client().ChangePassword(Convert.ToInt32(clientId), newPass, out message);

            if (response)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ClientId"] = clientId;
                ViewBag.Error = message;
                return View();
            }
        }

        /// <summary>
        /// Method to close session
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["Client"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}