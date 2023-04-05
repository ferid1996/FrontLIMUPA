using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIMUPA.Models
{
    public class Product 
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="required")]
        public string Name { get; set; }
       
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile Upload { get; set; }

        [Required(ErrorMessage = "required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "required")]
        public string Description { get; set; }
       
        public DateTime CreateDate { get; set; }
     
        public DateTime UpdateDate { get; set; }
        
        public bool Status { get; set; }
        public List<Sale> Sales { get; set; }
        public List<Saler> Salers { get; set; }
    }
}
