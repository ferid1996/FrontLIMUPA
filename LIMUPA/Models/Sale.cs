namespace LIMUPA.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int SalerId { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
        public Saler Saler { get; set; }
    }
}
