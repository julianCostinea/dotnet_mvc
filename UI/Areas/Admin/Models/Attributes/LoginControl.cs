using System.Web;
using System.Web.Mvc;
using DTO;

namespace UI.Areas.Admin.Models.Attributes
{
    public class LoginControl : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // if (!HttpContext.Current.User.Identity.IsAuthenticated)
            // {
            //     filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");
            // }
            if (UserStatic.UserID == 0)
            {
                filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");
            }
        }
    }
}