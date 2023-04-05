using LIMUPA.Models;

namespace LIMUPA.ViewModel
{
    public class AdminViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<User> Users { get; set; }
        public List<User> Admins { get; set; }  
    }
    
}
