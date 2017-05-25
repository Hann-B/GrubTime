using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using GrubTime.Models;
using System.Security.Claims;

namespace GrubTime.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
      
        // GET: api/Account
        //get all profiles
        //[HttpGet]
        //public IEnumerable<UserProfile> Get()
        //{
        //    return HttpContext.User.Identities.;
        //}


        // GET: api/Account/5
        //get one profile given and id
        [Authorize]
        [HttpGet("{id}", Name = "Get")]
        public UserProfile Get(int id)
        {
            return new UserProfile()
            {
                Id = id,
                Username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                Avatar = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            };
        }
        
        ////create a profile
        //[Authorize]
        //[HttpPost]
        //public UserProfile Create([FromBody]string profile)
        //{
        //    return new UserProfile
        //    {
        //        Id=
        //    };
        //}

        // POST: api/Account
        [Authorize]
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }
        
        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
