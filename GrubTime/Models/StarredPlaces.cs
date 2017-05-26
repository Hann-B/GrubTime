using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.Models
{
    public class StarredPlaces
    {
        public int Id { get; set; }

        //Restaurant
        public string PlaceId { get; set; }
        public bool IsStarred { get; set; } = false;
        //User
        public string UserToken { get; set; }
    }
}
