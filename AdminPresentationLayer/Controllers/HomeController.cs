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

        [HttpGet]
        public JsonResult GetUsers()
        {
            List<App_User> oList = new List<App_User>();
            oList = new BL_User().GetUsers();

            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }
    }
}