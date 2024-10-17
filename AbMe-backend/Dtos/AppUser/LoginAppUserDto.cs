using System.ComponentModel.DataAnnotations;

namespace AbMe_backend.Dtos.AppUser
{
    public class LoginAppUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }        
    }
}