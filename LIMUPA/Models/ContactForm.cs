using System.ComponentModel.DataAnnotations;
namespace LIMUPA.Models
{
    public class ContactForm
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage ="The email address is required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
       
        public string Email { get; set; }
        [Required(ErrorMessage ="the subject is required")]
        [MaxLength(30)]
        public string Subject { get; set; }
        [Required(ErrorMessage = "the text is required")]
        [MaxLength(500,ErrorMessage ="The text is very long")]
        public string Text { get; set; }
    }
}
