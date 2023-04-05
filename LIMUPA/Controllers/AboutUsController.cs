using LIMUPA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LIMUPA.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly DataContext _db;

        public AboutUsController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            AboutUs aboutus = _db.AboutUs.First();
            return View(aboutus);
        }
    }
}
