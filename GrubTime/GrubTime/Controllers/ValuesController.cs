using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrubTime.Controllers
{
    /// <summary>
    /// Home Controller -- no function
    /// </summary>
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return RedirectToRoute("/swagger/");
        }
    }
}