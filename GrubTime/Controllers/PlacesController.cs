using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using GrubTime.Models;
using GrubTime.ViewModels;
using GrubTime.Middleware;
using System.Collections;

namespace GrubTime.Controllers
{
    [Produces("application/json")]
    [Route("api/Places")]
    public class PlacesController : Controller
    {
        //GET: api/Places
        [HttpGet]
        public async Task<IEnumerable<PlacesVM>> Get(string latitude, string longitude)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync
                    (string.Format
                    ("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius=500&type=restaurant&key=AIzaSyAU8uC7NC2lOPh3-MDMLEwKelbCwIL28J4"
                    , latitude, longitude));
                var result = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response);
                return result.results.Select(s => new PlacesVM(s));
            }
        }

        //Middleware used to filter search query
        [MiddlewareFilter(typeof(MyFirstMiddleware))]
        [HttpGet]
        public IActionResult Index(string Latitude, string Longitude, int radius)
        {
            //Result list of places
            var places=0;
            //return list of places as JSON
            return Json(places);
        }
        
    }
}
