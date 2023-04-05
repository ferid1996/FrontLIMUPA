using LIMUPA.DTO;
using LIMUPA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using CryptoHelper;
using LIMUPA.Areas.Admin.Filters;

namespace LIMUPA.Areas.Admin.Controllers
{
    [Area("Admin")]
  
    public class UserController : Controller
    {
        private readonly DataContext _db;
        private User _user => RouteData.Values["loginnedUser"] as User;

        public UserController(DataContext db)
        {
            _db = db;
            
        }
       
        //public IActionResult GetUsers()
        //{
        //    List<User> users = _db.Users.ToList();
        //    return View(users);
        //}
        public IActionResult Login()
        {
            

            if (Request.Cookies.TryGetValue("token", out string token))
            {
                User user = _db.Users.FirstOrDefault(x => x.Token == token);
                if(user is object)
                {
                    return RedirectToAction("index", "home", "admin");
                }
                return View();
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] LoginDTO request)
        {
            if (ModelState.IsValid)
            {
                User user = _db.Users.FirstOrDefault(x=>x.Username == request.Username);
                if(user is object || user.UserTypeId==2 || Crypto.VerifyHashedPassword(user.Password, request.Password))
                {
                    user.Token = Guid.NewGuid().ToString();
                    _db.SaveChanges();
                    Response.Cookies.Append("token", user.Token, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = request.RememberMe ? DateTime.Now.AddDays(1) : null,
                        HttpOnly = true
                    });
                    return RedirectToAction("index","Home","Admin");
                }
                return View(request);
            }
            return View(request);
        }
        public IActionResult Logout()
        {
            Request.Cookies.TryGetValue("token",out string token);
            User user = _db.Users.FirstOrDefault(x => x.Token == token);
            if (user == null)
            {
                return RedirectToAction("login", "user", "admin");
            }
            user.Token = null;
            _db.SaveChanges();
            Response.Cookies.Delete("token");
            return RedirectToAction("login", "user", "admin");
        }
    }
}
