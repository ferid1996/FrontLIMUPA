using System.ComponentModel.DataAnnotations.Schema;

namespace LIMUPA.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Upload { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
