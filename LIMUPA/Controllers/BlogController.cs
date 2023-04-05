using LIMUPA.Filters;
using LIMUPA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LIMUPA.Controllers
{
    //[TypeFilter(typeof(Auth))]
    public class BlogController : Controller
    {
        private readonly DataContext _db;
        private User _user => RouteData.Values["loginnedUser"] as User;
        public BlogController(DataContext db)
        {
            _db = db;
        }
        public IActionResult BlogList()
        {
            List <Blog> blogs = _db.Blogs.ToList();
            return View(blogs);
        }
        public IActionResult Blogdetail(int id)
        {
            Blog blog = _db.Blogs.Find(id);

            return View(blog);

        }
    }
}
