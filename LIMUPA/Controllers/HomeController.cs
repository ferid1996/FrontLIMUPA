using LIMUPA.Filters;
using LIMUPA.Models;
using LIMUPA.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;

namespace LIMUPA.Controllers
{
    //[TypeFilter(typeof(Auth))]

    public class HomeController : Controller
    {
        private User _user => RouteData.Values["loginnedUser"] as User;
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _db;
        public HomeController(ILogger<HomeController> logger, DataContext db)
        {
            _logger = logger;
            _db = db;
        }



        [HttpGet]
        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();
          
            List<Product> product = _db.Products.OrderByDescending(m=>m.Id).ToList();
            List<Blog> blog = _db.Blogs.OrderByDescending(c => c.Id).ToList();
            //List<Sale> bestSellerCategories = _db.Sales.Include(x => x.Category).GroupBy(x => x.CategoryId).ToList();
            List<Slider> slide = _db.Sliders.ToList();
            var x = _db.Products;

            //model.BestSellerCategories = bestSellerCategories;           
            model.Sliders = slide;
            model.Products = product;
            model.Blogs = blog;
            if (_user is object)
            {
                ViewBag.User = _user;
            }
            return View(model);
        }
      
        public IActionResult ProductIndexJson()
        {
            var x = _db.Products.OrderByDescending(x => x.CreateDate).Take(6);        
            var json = JsonSerializer.Serialize(x);
            return Json(json);
        }
        public IActionResult Search([FromQuery] string productname)
        {
            List<Product> product = _db.Products.Where(x => x.Name.Contains(productname)).ToList();
            if(product.Count == 0)
            {
                return RedirectToAction("searchh");
            }
            return View(product);
           
        }
        public IActionResult Searchh()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}