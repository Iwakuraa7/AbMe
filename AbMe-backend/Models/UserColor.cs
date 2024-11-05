using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbMe_backend.Models
{
    public class UserColor
    {
        public int Id { get; set; }
        public string FirstColor { get; set; } = "#ff6347";
        public string SecondColor { get; set; } = "#ffd700";
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}