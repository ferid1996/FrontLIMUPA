 using System.ComponentModel.DataAnnotations;

namespace LIMUPA.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(25, ErrorMessage = "Username dont be  more 25 letters")]
        [MinLength(8, ErrorMessage = "Username dont be less 8 letters")]
        public string Username { get; set; }

       
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
