using System.ComponentModel.DataAnnotations;

namespace LIMUPA.Models
{
    public class UserType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
