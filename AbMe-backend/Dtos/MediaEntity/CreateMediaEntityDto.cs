using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbMe_backend.Dtos.MediaEntity
{
    public class CreateMediaEntityDto
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }         
    }
}