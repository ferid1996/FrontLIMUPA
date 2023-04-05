using LIMUPA.DTO;
using LIMUPA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CryptoHelper;
using LIMUPA.Filters;
using LIMUPA.ViewModel;

namespace LIMUPA.Controllers
{
  
    public class UserController : Controller
    {
        private User _user => RouteData.Values["loginnedUser"] as User;
        private readonly DataContext _db;
        public UserController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Register()
        {
            
            if (Request.Cookies.TryGetValue("token", out string token))
            {
                User user = _db.Users.FirstOrDefault(x => x.Token == token);
                if (user is object)
                {
                    ViewBag.User = user;
                    return RedirectToAction("index", "home");
                }
                return View();
            }        
             
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([FromForm] RegisterDTO request)
        {

            if (ModelState.IsValid)
            {                       
                if (_db.Users.Any(x=>x.Username==request.Username))
                {
                    ModelState.AddModelError("Username","The Username already is available");
                    return View();

                }
                User user = new User
                {
                    Username = request.Username,
                    UserTypeId=1,
                    Email = request.Email,
                    Password = Crypto.HashPassword(request.Password),
                    Token = Guid.NewGuid().ToString()
                };
                _db.Users.Add(user);
                _db.SaveChanges();
                Response.Cookies.Append("token", user.Token, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1),
                    HttpOnly = true
                }); ;
                return RedirectToAction("index","home");
            }
            return View(request);
            
           


            //TempData["Error"] = "Email olmalidir";



        }
      
        public IActionResult Login()
        {
           
            if (Request.Cookies.TryGetValue("token",out string token))
            {             
                    User user = _db.Users.FirstOrDefault(x => x.Token == token);
                    if (user is object)
                    {
                        return RedirectToAction("index", "home");
                    }
                    return View();
               
            }
            return View();


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([FromForm] LoginDTO request) 
        {
            if (ModelState.IsValid)
            {
                User user = _db.Users.FirstOrDefault(x=>x.Username==request.Username);
                if (user is object && Crypto.VerifyHashedPassword(user.Password,request.Password))
                {
                    user.Token = Guid.NewGuid().ToString();
                    _db.SaveChanges();
                    Response.Cookies.Append("token",user.Token,new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires=request.RememberMe?DateTime.Now.AddDays(1):null,
                        HttpOnly= true
                    });

                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError("Password", "The username and the password are wrong");
                return View(request);
            }
            return View(request);
        }
        
        public IActionResult Logout()
        {
            Request.Cookies.TryGetValue("token",out string token);
            var user = _db.Users.FirstOrDefault(x => x.Token == token);
            if (user is null)
            {
                return RedirectToAction("login", "user");
            }
            user.Token = null;  
            _db.SaveChanges();
            Response.Cookies.Delete("token");
            return RedirectToAction("login","user");
        }
        public IActionResult ForgottenPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgottenPassword(ForgottenPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _db.Users.FirstOrDefault(x=>x.Email==model.Email);
                if (user == null )
                {
                    return RedirectToAction("register","user");
                }


            }
            return View(model);
        }
    }
}
