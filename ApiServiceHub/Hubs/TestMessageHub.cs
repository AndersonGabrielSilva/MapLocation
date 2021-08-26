using MapLocation.Shared.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServiceHub.Hubs
{
    public class TestMessageHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await  Groups.AddToGroupAsync(Context.ConnectionId, (groupName));
        }


        public async Task SendMessageGroup(string groupName, string message)
        {
              await  Clients.Group(groupName).SendAsync(SignalRName.TestMessageHub,message);
        }
    }
}
