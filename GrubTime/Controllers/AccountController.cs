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
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Auth0.ManagementApi;

namespace GrubTime.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private ManagementApiClient apiClient;

        //get a token
        //public GetToken()
        //{
        //    var client = new RestClient("https://hlb.auth0.com/oauth/token");
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("content-type", "application/json");
        //    request.AddParameter("application/json", "{\"grant_type\":\"client_credentials\",\"client_id\": \"6d2XXZ-qUgQoye0ia03OkbG4Zu20dFj3\",\"client_secret\": \"YW0FmqDgNWPQNp78lwpJgY5dDfapvgwAqNnZi4kjy2iM1J7dNvI1abPMu0IMOYst\",\"audience\": \"https://hlb.auth0.com/api/v2/\"}", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //}

        // GET: api/Account
        //get all profiles
        //[Authorize("create")]
        //[HttpGet]
        //public async Task AllAccountsAsync()
        //{
        //    var apiClient = new ManagementApiClient("token", new Uri($"https://{Configuration["Auth0:Domain"]}/api/v2"));

        //    await apiClient.Clients.GetAllAsync();
        //}

        // GET: api/Account/5
        //get one profile given and id
        //[Authorize("read")]
        //[HttpGet]
        //public Object UserInformation()
        //{
        //    string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        //    string name = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        //    return 
        //}

        //create a profile
        //[HttpPost]
        //public UserProfile Create([FromBody]string profile)
        //{
        //    var pro = JsonConvert.DeserializeObject<UserProfile>(profile);
        //    var user = new UserProfile
        //    {
        //        Id = pro.Id,
        //        Username=pro.Username,
        //        Name=pro.Name,
        //        Avatar=pro.Avatar
        //    };
        //    return user;
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
