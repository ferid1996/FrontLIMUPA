using LIMUPA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NuGet.Common;

namespace LIMUPA.Filters
{
    public class Auth:ActionFilterAttribute
    {
        private readonly DataContext _db;
        public Auth(DataContext db)
        {
            _db = db;
        }
        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    if (!context.HttpContext.Request.Cookies.TryGetValue("token", out string token))
        //    {
        //        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login" }));
        //    }
        //    User user = _db.Users.FirstOrDefault(x => x.Token == token);
        //    if (user == null)
        //    {
        //        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "user", action = "login" }));
        //    }
        //    var controller = context.Controller as Controller;
        //    if (controller != null)
        //    {
        //        controller.ViewBag.User = user;
        //    }
        //    context.RouteData.Values["loginnedUser"] = user;
        //}
       
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Cookies.TryGetValue("token", out string token))
            {
                User user = _db.Users.FirstOrDefault(x => x.Token == token);
                if (user == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "user", action = "login" }));
                }
                var controller = context.Controller as Controller;
                if (controller != null)
                {
                    controller.ViewBag.User = user;
                }
                context.RouteData.Values["loginnedUser"] = user;
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login" }));
            }
           


            

            


        }
    }
}
