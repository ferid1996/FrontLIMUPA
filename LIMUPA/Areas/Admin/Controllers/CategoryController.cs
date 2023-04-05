using LIMUPA.Areas.Admin.Filters;
using LIMUPA.Filters;
using LIMUPA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LIMUPA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(AuthAdmin))]
    public class CategoryController : Controller
    {
        private readonly DataContext _db;
        private User _user => RouteData.Values["loginnedUser"] as User;
        public CategoryController(DataContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([FromForm] Category request)
        {
            var filename = DateTime.Now.ToString("yyyymmddhhnnssff") + Path.GetExtension(request.Upload.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                request.Upload.CopyTo(stream);
            }

            Category category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                CreateDate = DateTime.Now,
                Image = filename,
                Status = request.Status
            };
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = _db.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(int id, Category request)
        {
            Category category = _db.Categories.Find(id);

            if (request.Upload is object)
            {
                var filename = DateTime.Now.ToString("yyyymmddhhmmssff") + Path.GetExtension(request.Upload.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    request.Upload.CopyTo(stream);
                }

                category.Image = filename;
            }
            
            category.Name = request.Name;
            category.Update = DateTime.Now;
            category.Description = request.Description;
            category.Status = request.Status;

            _db.SaveChanges();
            return RedirectToAction("index");

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.Find(id);
            if(category is null) return NotFound();
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
