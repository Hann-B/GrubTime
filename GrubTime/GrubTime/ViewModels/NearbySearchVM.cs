using GrubTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.ViewModels
{
    /// <summary>
    /// Search area for restaurants
    /// </summary>
    public class NearbySearchVM
    {
        /// <summary>
        /// What is your longitude
        /// </summary>
        public decimal Longitude { get; set; }
        /// <summary>
        /// What is your latitude
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// What is the radius you would like to search
        /// </summary>
        public int Radius { get; set; } = 15;
        /// <summary>
        /// Your coordinates
        /// </summary>
        public string Location {
            get
            {
                return $"{Latitude},{Longitude}";
            }
        }
    }
}
