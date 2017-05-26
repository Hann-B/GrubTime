using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GrubTime.Middleware;
using GrubTime.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using GrubTime.ViewModels;
using Microsoft.AspNetCore.Http.Internal;
using System.Net;
using static GrubTime.Models.PlaceDetails;
using Microsoft.Extensions.Options;

namespace GrubTime.Controllers
{
    [Produces("application/json")]
    [Route("api/Places")]
    public class PlacesController : Controller
    {
        readonly private Google _google;
        public PlacesController(IOptions<Google> optionsAccessor)
        {
            _google = optionsAccessor.Value;
        }
        //Nearby Searches
        // POST: api/Places
        [HttpPost]
        public StatusCodeResult Post([FromBody]string value)
        {
            return Ok();
        }

        //Restuarant details
        [HttpGet]
        public async Task<object> DetailsAsync(string id)
        {

            var detailApiUrl = string.Format(_google.Details, id);
            HttpWebRequest query = (HttpWebRequest)WebRequest.Create(detailApiUrl);
            WebResponse response = await query.GetResponseAsync();
            var raw = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
            {
                raw = reader.ReadToEnd();
            }
            var allresults = JsonConvert.DeserializeObject<RootObject>(raw);

            return allresults;
        }
    }
}

