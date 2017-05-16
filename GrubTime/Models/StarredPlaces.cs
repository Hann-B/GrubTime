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
        public int PlaceId { get; set; }
        public Restaurant StarredPlace { get; set; }
        //User
        public int UserId { get; set; }
        public UserProfile Profile { get; set; }
    }
}
