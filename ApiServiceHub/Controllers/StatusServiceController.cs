using ApiServiceHub.Hubs;
using MapLocation.Shared.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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


        [HttpGet("teste")]
        public async Task<ActionResult> GetTeste([FromServices] IHubContext<TestMessageHub> context, string id, string Message)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(Message))
                return Ok(new { id="Informe o ID", Message = "Exemplo " });
            
            await  context.Clients.Group(id).SendAsync(SignalRName.TestMessageHub, Message);

            return Ok("Mensagem enviada");
        }

    }
}
