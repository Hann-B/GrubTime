using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Net;
using GrubTime.Models;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using Newtonsoft.Json;

namespace GrubTime.Controllers
{
    [Produces("application/json")]
    [Route("api/Favorites")]
    public class FavoritesController : Controller
    {
        private readonly GrubTimeContext _context;

        public FavoritesController(GrubTimeContext context)
        {
            _context = context;
        }

        // GET: api/Favorites
        [HttpGet]
        public IEnumerable<StarredPlaces> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //get all starred places for a user
            return _context.StarredPlaces
                .Where(u => u.UserToken == userId)
                .ToList();
        }

        // POST: api/Favorites
        [HttpPut]
        [Authorize]
        public void Post([FromBody]StarredPlaces starredPlace)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var starred = _context.StarredPlaces.FirstOrDefault(f => f.PlaceId == starredPlace.PlaceId && f.UserToken == userId);
            if (starred == null)
            {
                // create it
                var StarredPlace = new StarredPlaces
                {
                    PlaceId = starredPlace.PlaceId,
                    IsStarred = starredPlace.IsStarred,
                    UserToken = userId
                };

                if (StarredPlace.IsStarred)
                {
                    // add it
                    _context.StarredPlaces.Add(StarredPlace);
                }
            }
            else if (starred!=null && !starredPlace.IsStarred)
            {
                // remove it
                _context.StarredPlaces.Remove(starred);
            }
            _context.SaveChanges();
        }
    }
}
