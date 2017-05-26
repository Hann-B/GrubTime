using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.Models
{
    public class PlaceDetails
    {
        public class AddressComponent
        {
            public string long_name { get; set; }
            public string short_name { get; set; }
            public List<string> types { get; set; }
        }

        public class DetailLocation
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Northeast
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Southwest
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Viewport
        {
            public Northeast northeast { get; set; }
            public Southwest southwest { get; set; }
        }

        public class DetailGeometry
        {
            public Location location { get; set; }
            public Viewport viewport { get; set; }
        }

        public class AltId
        {
            public string place_id { get; set; }
            public string scope { get; set; }
        }

        public class Aspect
        {
            public int rating { get; set; }
            public string type { get; set; }
        }

        public class Review
        {
            public List<Aspect> aspects { get; set; }
            public string author_name { get; set; }
            public string author_url { get; set; }
            public string language { get; set; }
            public int rating { get; set; }
            public string text { get; set; }
            public object time { get; set; }
        }

        public class DetailResult
        {
            public List<AddressComponent> address_components { get; set; }
            public string adr_address { get; set; }
            public string formatted_address { get; set; }
            public string formatted_phone_number { get; set; }
            public Geometry geometry { get; set; }
            public string icon { get; set; }
            public string id { get; set; }
            public string international_phone_number { get; set; }
            public string name { get; set; }
            public string place_id { get; set; }
            public string scope { get; set; }
            public List<AltId> alt_ids { get; set; }
            public double rating { get; set; }
            public string reference { get; set; }
            public List<Review> reviews { get; set; }
            public List<string> types { get; set; }
            public string url { get; set; }
            public string vicinity { get; set; }
            public string website { get; set; }
        }

        public class RootObject
        {
            public List<object> html_attributions { get; set; }
            public Result result { get; set; }
            public string status { get; set; }
        }
    }
}
