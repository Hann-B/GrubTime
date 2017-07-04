using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GrubTime.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace GrubTime.Controllers
{
    /// <summary>
    /// Directions to your restaurant of liking from given coordinates
    /// </summary>
    [Produces("application/json")]
    [Route("api/Directions")]
    public class DirectionsController : Controller
    {
        readonly private Google _google;
        /// <summary>
        /// Google API query to navigate to your chosen restaurant
        /// </summary>
        /// <param name="optionsAccessor"></param>
        public DirectionsController(IOptions<Google> optionsAccessor)
        {
            _google = optionsAccessor.Value;
        }

        /// <summary>
        /// Directions to the restaurant from given coordinates
        /// </summary>
        /// <param name="location"></param>
        /// <param name="placeId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> DirectionsAsync(string location, string placeId)
        {
            
            var DirectionApiUrl = string.Format(_google.Directions, location, placeId);
            HttpWebRequest query = (HttpWebRequest)WebRequest.Create(DirectionApiUrl);
            WebResponse response = await query.GetResponseAsync();
            var raw = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
            {
                raw = reader.ReadToEnd();
            }
            var allresults = JsonConvert.DeserializeObject<DirectionsRootObject>(raw);

            return allresults;
        }
    }
}