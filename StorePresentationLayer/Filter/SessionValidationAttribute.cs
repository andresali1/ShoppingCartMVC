using System.Web;
using System.Web.Mvc;

namespace StorePresentationLayer.Filter
{
    public class SessionValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(HttpContext.Current.Session["Client"] == null)
            {
                filterContext.Result = new RedirectResult("~/Access/Index");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}