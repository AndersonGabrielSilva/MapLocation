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
    public class MensageController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(new Message());
        }


        [HttpPost]
        public async Task<ActionResult> GetTeste([FromServices] IHubContext<TestMessageHub> context, Message message)
        {
            if (message == null || string.IsNullOrEmpty(message.Id) || string.IsNullOrEmpty(message.Messagem))
                return Ok(new Message());

            await context.Clients.Group(message.Id).SendAsync(SignalRName.TestMessageHub, message.Messagem);

            return Ok("Mensagem enviada");
        }

    }
}
