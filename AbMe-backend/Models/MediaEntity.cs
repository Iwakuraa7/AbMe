using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbMe_backend.Models
{
    public class MediaEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }        
    }
}