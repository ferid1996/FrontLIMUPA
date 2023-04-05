using System.ComponentModel.DataAnnotations;

namespace LIMUPA.Models
{
    public class User
    {
        public int Id { get; set; }
      
        public int UserTypeId { get; set; }

        public UserType UserType { get; set; }

        public string Username { get; set; }       

        public string Email { get; set; }

        public string? Token { get; set; }
       
        public string Password { get; set; }

       

        

    }
}
