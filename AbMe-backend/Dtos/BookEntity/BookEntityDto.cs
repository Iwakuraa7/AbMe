using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbMe_backend.Dtos.BookEntity
{
    public class BookEntityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public string AppUserId { get; set; }        
    }
}