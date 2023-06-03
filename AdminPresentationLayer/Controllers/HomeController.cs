using BusinessLayer;
using EntityLayer;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AdminPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        /// <summary>
        /// Method to get all users in db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUsers()
        {
            List<App_User> oList = new List<App_User>();
            oList = new BL_User().GetUsers();

            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to save an user in DB, Created or edited
        /// </summary>
        /// <param name="obj">Object with the user's data</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveUser(App_User obj)
        {
            object result;
            string message = string.Empty;

            if(obj.UserId == 0)
            {
                result = new BL_User().AddUser(obj, out message);
            }
            else
            {
                result = new BL_User().EditUser(obj, out message);
            }

            return Json(new { result, message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to delete an user from Admin page
        /// </summary>
        /// <param name="userId">user's Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteUser(int userId)
        {
            bool response = false;
            string message = string.Empty;

            response = new BL_User().DeleteUser(userId, out message);

            return Json(new { result = response, message }, JsonRequestBehavior.AllowGet);
        }
    }
}