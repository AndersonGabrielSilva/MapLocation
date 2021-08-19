using DapperRepository.Models;
using DapperRepository.Repositories;
using MapLocation.Shared.SignalR;
using MapLocationShared.Model;
using MapLocationShared.Utils;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiServiceHub.Hubs
{
    public class LocationHub : BaseHub
    {
        #region Gerenciador de grupos
        public override Task JoinGroup(string groupName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, (groupName + SignalRName.LocationHub));
        }

        public override Task LeaveGroup(string groupName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, (groupName + SignalRName.LocationHub));
        }
        #endregion

        #region Mensagens
        //Este método pode ser invocado pelo Client via SignalR 
        public override async Task SendMessageGroup(string groupName, string message)
        {
            //Notifica todos os Hubs conectados
            await Clients.All.SendAsync(groupName + SignalRName.LocationHub, message);
        }
        #endregion

        public async Task SaveLocationNotify(string groupName, LocationGPS location)
        {
            DataSettings.GetConnection();

            var repository = new LocationGpsRepository(DataSettings.GetConnection());

            repository.Create(new LocationGPSDapper(location));

            await Clients.All.SendAsync(groupName + SignalRName.LocationHub, new List<LocationGPS> { location });
        }
    }
}
