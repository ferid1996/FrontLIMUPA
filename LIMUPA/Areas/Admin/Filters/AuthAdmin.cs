using LIMUPA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LIMUPA.Areas.Admin.Filters
{
    public class AuthAdmin:ActionFilterAttribute
    {
        private readonly DataContext _db;
        public AuthAdmin(DataContext db)
        {
            _db = db;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Cookies.TryGetValue("token", out string token))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "login", controller = "user", }));
            }
            User user = _db.Users.FirstOrDefault(x => x.Token == token);

            if (user != null)
            {
                if (user.UserTypeId == 2)
                {
                    var controller = context.Controller as Controller;
                    if (controller != null)
                    {
                        controller.ViewBag.User = user;
                    }
                    context.RouteData.Values["loginnedUser"] = user;
                    
                }
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "login", controller = "user" }));
            }

        }

    }
}

