using System.ComponentModel.DataAnnotations.Schema;

namespace LIMUPA.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Upload { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }

    }
}
