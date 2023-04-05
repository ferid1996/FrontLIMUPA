using LIMUPA.Areas.Admin.Filters;
using LIMUPA.DTO;
using LIMUPA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LIMUPA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(AuthAdmin))]
    public class SliderController : Controller
    {
        private readonly DataContext _db;
        private User _user => RouteData.Values["loginnedUser"] as User;
        public SliderController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Slider> sliders = _db.Sliders.ToList();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([FromForm] SliderDTO request)
        {
            if (ModelState.IsValid)
            {
                var filename = DateTime.Now.ToString("yyyymmddhhmmssff") + Path.GetExtension(request.Upload.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    request.Upload.CopyTo(stream);
                }

                Slider slider = new Slider
                {
                    Image = filename,
                    Title = request.Title,
                    Text = request.Text,
                    Price = request.Price,
                    CreateDate = DateTime.Now,
                    Status = request.Status
                };
                _db.Sliders.Add(slider);

                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(request);
           
        }

    }
}
