using LIMUPA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LIMUPA.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _db;

        public CategoryController(DataContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
           
            List<Category> categories = _db.Categories.Where(c=>c.Status == true).ToList();

            return View(categories); 
        }
        public IActionResult Products(int id)
        {
            List<Product> products = _db.Products.Where(x => x.CategoryId == id).ToList();
           
            return View(products);
        }
    }
}
