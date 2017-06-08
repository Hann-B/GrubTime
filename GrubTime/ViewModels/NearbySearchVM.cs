using GrubTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.ViewModels
{
    public class NearbySearchVM
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int Radius { get; set; } = 15;
        public string Location {
            get
            {
                return $"{Latitude},{Longitude}";
            }
        }
    }
}
