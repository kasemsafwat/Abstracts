using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Abstracts.Models.Service
{
    public class Authenticate : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.HttpContext.Session["UserID"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "controller", "Home" }, { "action", "Login" } });
                return;
            }
            if (filterContext.HttpContext.Session["menus"] == null) return;

            var menus = JsonConvert.DeserializeObject<List<string>>((string)filterContext.HttpContext.Session["menus"]);

            var ControllerName = filterContext.RouteData.Values["controller"];
            var ActionName = filterContext.RouteData.Values["action"];


            string url = "/" + ControllerName + "/" + ActionName;
            if (!menus.Where(s => s.ToLower() == url.ToLower()).Any())
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "controller", "Home" }, { "action", "AccessDenied" } });
                return;
            }
        }
    }
}