using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIMUPA.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string? Image { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime Update { get; set; }
        [Required]
        public bool Status { get; set; }
        [NotMapped]
        public IFormFile Upload { get; set; }
        public List<Product> Products { get; set; }

        public List<Sale> Sales { get; set; }
    }
}
