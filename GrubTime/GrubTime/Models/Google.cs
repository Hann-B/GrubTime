using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.Models
{
    /// <summary>
    /// Google APIs
    /// </summary>
    public class Google
    {
        /// <summary>
        /// Google Places API
        /// </summary>
        public String Nearby { get; set; }
        /// <summary>
        /// Google Place Details API
        /// </summary>
        public String Details { get; set; }
        /// <summary>
        /// Google Directions API
        /// </summary>
        public String Directions { get; set; }
    }
}
