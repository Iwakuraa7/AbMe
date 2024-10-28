using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbMe_backend.Dtos.BookEntity
{
    public class CreateBookEntityDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }        
    }
}