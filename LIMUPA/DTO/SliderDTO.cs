using System.ComponentModel.DataAnnotations.Schema;

namespace LIMUPA.DTO
{
    public class SliderDTO
    {
        [NotMapped]
        public IFormFile Upload { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
    }
}
