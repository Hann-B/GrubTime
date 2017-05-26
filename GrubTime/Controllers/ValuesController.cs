using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GrubTime.Controllers
{
    [Route("api")]
    public class ValuesController : Controller
    {
        [Authorize]
        [HttpGet]
        [Route("ping")]
        public string Ping()
        {
            return "Pong";
        }
    }
}
