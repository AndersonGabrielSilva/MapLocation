using ApiServiceHub.Hubs;
using MapLocation.Shared.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ApiServiceHub.Controllers
{
    public class LocationController : BaseController
    {
        #region Atributos
        private readonly IHubContext<LocationHub> hubContext;
        #endregion

        #region Construtor
        public LocationController(IHubContext<LocationHub> hubContext)
        {
            this.hubContext = hubContext;
        }
        #endregion

        [HttpPost()]
        public async Task<ActionResult> Post(string group, string mensagem)
        {
            await hubContext.Clients.All.SendAsync(group + SignalRName.RouteLocationHub, mensagem);
            return Ok();
        }

    }
}
