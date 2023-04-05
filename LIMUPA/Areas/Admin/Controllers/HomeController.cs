using LIMUPA.Areas.Admin.Filters;
using LIMUPA.Filters;
using LIMUPA.Models;
using LIMUPA.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;

namespace LIMUPA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(AuthAdmin))]
    public class HomeController : Controller
    {
        private User _user => RouteData.Values["loginnedUser"] as User;
        private readonly DataContext _db;
        public HomeController(DataContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            List<User> users = _db.Users.ToList();
            List<Category> categories = _db.Categories.ToList();
            List<Product> products = _db.Products.ToList();
            List<Blog> blogs = _db.Blogs.ToList();
            List<Slider> sliders = _db.Sliders.ToList();
            List<User> admins = _db.Users.Where(x=>x.UserTypeId==2).ToList();
            adminViewModel.Admins = admins;
            adminViewModel.Users=users;
            adminViewModel.Sliders = sliders;
            adminViewModel.Categories=categories;
            adminViewModel.Blogs=blogs;
            adminViewModel.Products=products;
            return View(adminViewModel);

        }
        public IActionResult Users(int pg = 1)
        {
            List<User> users =_db.Users.ToList();
            const int pageSize = 3;
            if (pg < 1)
                pg = 1;
            int rescCount = users.Count();
            var pager = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = users.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
        }
        public PartialViewResult SearchUsers(string textuser)
        {
            List<User> users = _db.Users.ToList();
            var result = users.Where(x => x.Username.ToLower().Contains(textuser)).ToList();
            return PartialView("_partialUsers", result);
        }
        public IActionResult Admins()
        {
            List<User> users = _db.Users.Where(x=>x.UserTypeId==2).ToList();
            return View(users);
        }
        public IActionResult SearchUser(string user)
        {
            List<User> users = _db.Users.Where(x => x.Username.Contains(user)).ToList();
            if (users is object)
            {
                
                return View();
            }
            return RedirectToAction("Admins");
        }
        

    }
}
