using LIMUPA.Models;

namespace LIMUPA.ViewModel
{
    public class ViewModel : IViewModel
    {
        public List<Slider>? Sliders { get; set; }
        public List<Product>? Products { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Blog>? Blogs { get; set; }
        public List<User>? Users { get; set; }
        public List<User>? Admins { get; set; }
        public ContactForm? ContactForms { get; set; }
        public ContactUs? Contacts { get; set; }
        public List<Sale>? BestSellerCategories { get; set; }
    }
}
