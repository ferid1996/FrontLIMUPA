using System.ComponentModel.DataAnnotations;

namespace LIMUPA.DTO
{
    public class RegisterDTO
    {      
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(25, ErrorMessage = "Username dont be  more 25 letters")]
        [MinLength(8, ErrorMessage = "Username dont be less 8 letters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]

        public string Email { get; set; }

       

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password didn't repeat right")]
        public string ConfirmPassword { get; set; }

    }
}
