using System.ComponentModel.DataAnnotations;

namespace AbMe_backend.Dtos.AppUser
{
    public class CreateAppUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}