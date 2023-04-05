using LIMUPA.Areas.Admin.Filters;
using LIMUPA.DTO;
using LIMUPA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LIMUPA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(AuthAdmin))]
    public class BlogController : Controller
    {
        private readonly DataContext _db;
        private User _user => RouteData.Values["loginnedUser"] as User;
        public BlogController(DataContext db )
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Blog> blogs = _db.Blogs.ToList();
            return View(blogs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([FromForm] BlogDTO request)
        {  

            if (ModelState.IsValid)
            {
                var filename = DateTime.Now.ToString("yyyymmddhhmmssff") + Path.GetExtension(request.Upload.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    request.Upload.CopyTo(stream);
                }
                Blog blog = new Blog
                {
                    Title = request.Title,
                    Image = filename,
                    Description = request.Description,
                    CreateDate = DateTime.Now,
                    Status = request.Status
                };
                _db.Blogs.Add(blog);
                _db.SaveChanges();
                return RedirectToAction("index");
            }
            
            return View(request);
           
        }
        public IActionResult Edit(int id)
        {
            Blog blog = _db.Blogs.Find(id);
            if(blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        [HttpPost]
        public IActionResult Edit(int id, BlogDTO request)
        {
            Blog blog = _db.Blogs.Find(id);
            if (blog is object)
            {
                if (ModelState.IsValid)
                {
                    var filename=DateTime.Now.ToString("yyyymmddhhmmssff")+Path.GetExtension(request.Upload.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
                    using (var stream = new FileStream(path,FileMode.Create))
                    {
                        request.Upload.CopyTo(stream);
                    }
                    blog.Title = request.Title;
                    blog.Image = filename;
                    blog.Description = request.Description;
                    blog.Status = request.Status;
                    _db.SaveChanges();
                   return  RedirectToAction("index");


                }
                return View(request);
            }
            return NotFound();
           

        } 
    
        public IActionResult Delete(int id)
        {
            Blog blog=_db.Blogs.Find(id);
            if (blog is null) return NotFound();
            _db.Blogs.Remove(blog);
            _db.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
