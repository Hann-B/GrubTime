using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string PlaceId { get; set; }
        public bool IsStarred { get; set; } = false;
    }
}
