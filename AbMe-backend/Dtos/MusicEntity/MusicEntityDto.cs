using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbMe_backend.Dtos.MusicEntity
{
    public class MusicEntityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public string ImageUrl { get; set; }
        public string AppUserId { get; set; }        
    }
}