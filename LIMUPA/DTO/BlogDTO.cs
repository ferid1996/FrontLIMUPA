using System.ComponentModel.DataAnnotations.Schema;

namespace LIMUPA.DTO
{
    public class BlogDTO
    {
        public string Title { get; set; }     
     
        [NotMapped]
        public IFormFile Upload { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
