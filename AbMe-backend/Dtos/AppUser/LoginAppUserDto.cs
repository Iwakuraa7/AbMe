using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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