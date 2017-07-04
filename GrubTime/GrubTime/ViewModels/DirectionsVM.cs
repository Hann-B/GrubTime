using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.ViewModels
{
    /// <summary>
    /// Get Directions
    /// </summary>
    public class DirectionsVM
    {
        /// <summary>
        /// What is your longitude
        /// </summary>
        public decimal Longitude { get; set; }
        /// <summary>
        /// What is you latitude
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// Your Coordinates
        /// </summary>
        public string Location
        {
            get
            {
                return $"{Latitude},{Longitude}";
            }
        }
    }
}
