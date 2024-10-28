using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AbMe_backend.Models
{
    public class AppUser : IdentityUser
    {
        public List<MusicEntity> MusicEntities { get; set; }
        public List<BookEntity> BookEntities { get; set; }
    }
}