using LIMUPA.Areas.Admin.Filters;
using LIMUPA.Models;
using LIMUPA.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIMUPA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(AuthAdmin))]
    public class ProductController : Controller
    {
        private readonly DataContext _db;
        private User _user => RouteData.Values["loginnedUser"] as User;
        public ProductController(DataContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var products = _db.Products.Include("Category").ToList();
            return View(products);
        }
        public IActionResult Create()
        {
           
            List<Category> category = _db.Categories.ToList();
          
            return View(category);
        }
        [HttpPost]
        public IActionResult Create([FromForm] string name,string description ,IFormFile upload,int price,bool status,int categoryid)
        {
            var filename = DateTime.Now.ToString("yyyymmddhhmmssff") + Path.GetExtension(upload.FileName);
            var path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","uploads", filename);
            using (var stream = new FileStream(path,FileMode.Create))
            {
                upload.CopyTo(stream);
            }
              
            Product product = new Product {
            Image=filename,
            CategoryId=categoryid,
            Name = name,
            Description = description,
            Price = price,
            CreateDate = DateTime.Now,
            Status = status
            };
            _db.Products.Add(product);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            List<Category> category = _db.Categories.ToList();

            var product = _db.Products.Find(id);
             productViewModel.Product=product;
            productViewModel.Categories=category;
            return View(productViewModel);                      
                

        }
        [HttpPost]
        public IActionResult Edit(int id,[FromForm] ProductViewModel productViewModel)
        {
            Product product = _db.Products.Find(id);

                product.Name = productViewModel.Product.Name;
                product.Description = productViewModel.Product.Description;
                product.Price = productViewModel.Product.Price;
                product.UpdateDate = DateTime.Now;
                product.Status = productViewModel.Product.Status;
            product.CategoryId = productViewModel.Product.CategoryId;
                

            
            if (productViewModel.Product.Upload is object)
            {
                var filename = DateTime.Now.ToString("yyyymmddhhmmssff") + Path.GetExtension(productViewModel.Product.Upload.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    productViewModel.Product.Upload.CopyTo(stream);
                }

                product.Image = filename;
            }
            _db.SaveChanges();
            return RedirectToAction("index");
          


        }
       
        public IActionResult Delete(int id)
        {
            Product product=_db.Products.Find(id);
            if (product is null) return NotFound();
            _db.Products.Remove(product);
            _db.SaveChanges();

            return RedirectToAction("index");

        }


    }
}
