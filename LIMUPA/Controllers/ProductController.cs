using LIMUPA.Filters;
using LIMUPA.Models;
using LIMUPA.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LIMUPA.Controllers
{
    //[Authorize]
    //[TypeFilter(typeof(Auth))]
    public class ProductController : Controller
    {
        private User _user => RouteData.Values["loginnedUser"] as User;
        private readonly DataContext _db;

        public ProductController(DataContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult ProductList()
        {
            IndexViewModel model = new IndexViewModel();
            List<Product> products = _db.Products.ToList();
            List<Slider> sliders = _db.Sliders.ToList();
            model.Sliders = sliders;
            model.Products=products;
            return View(model);

        }
        public IActionResult Detail(int id)
        {

            Product product = _db.Products.Find(id);

            if (product is object)
            {
                return View(product);
            }
            return NotFound();
        }

    }
}
