using LIMUPA.Models;

namespace LIMUPA.ViewModel
{
    public interface IViewModel
    {
        List<Slider>? Sliders { get; set; }
        List<Product>? Products { get; set; }
        List<Category>? Categories { get; set; }
        List<Blog>? Blogs { get; set; }
        List<User>? Users { get; set; }
        List<User>? Admins { get; set; }
        ContactForm? ContactForms { get; set; }
        ContactUs? Contacts { get; set; }
        List<Sale>? BestSellerCategories { get; set; }

    }
}
