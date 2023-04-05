using Microsoft.EntityFrameworkCore;

namespace LIMUPA.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Blog> Blogs  { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Product> Products  { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<CustomerMessage> CustomerMessages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Saler> Salers { get; set; }

    }
}
