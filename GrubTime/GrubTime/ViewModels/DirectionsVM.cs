using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.ViewModels
{
    public class DirectionsVM
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string Location
        {
            get
            {
                return $"{Latitude},{Longitude}";
            }
        }
    }
}
