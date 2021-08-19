using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServiceHub.Controllers
{
    [Route("[controller]")]
    public class StatusServiceController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {            
            return Ok("Status : Online");
        }
    }
}
