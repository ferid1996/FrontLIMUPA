using LIMUPA.Models;

namespace LIMUPA.ViewModel
{
    public class IndexViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Product> Products { get; set; }
        public List<Sale> BestSellerCategories { get; set; }
        public List<Blog> Blogs { get; set; }

    }
}
