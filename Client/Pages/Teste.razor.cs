using BlazorGPS.Client.Utils;
using MapLocation.Shared.SignalR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorGPS.Client.Pages
{
    public class TesteBase : Base

    {
        public string ID { get; set; }

        public List<string> Message { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    if (firstRender)
                    {
                        #region SignalR
                        //var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
                        //var group = authenticationState.User.Claims.SingleOrDefault(x => x.Type == nameof(SignalRName.Group)).Value.ToString();

                        var url = Configuration.GetSection("ApiServiceHub").GetSection("ApiUrl").Value;

                        ID = Guid.NewGuid().ToString();

                        var HubConnection = new HubConnectionBuilder()
                                .WithUrl(url + SignalRName.RouteTesteHub)
                                .Build();



                        HubConnection.On<string>(SignalRName.TestMessageHub, (message) => RecevedMessage(message));

                        await HubConnection.StartAsync();

                        await HubConnection.SendAsync(SignalRName.JoinGroup, ID);
                        StateHasChanged();
                        #endregion
                    }
                }
                catch (Exception erro)
                {
                    var msgErro = erro.Message;
                }
            }
        }

        public void RecevedMessage(string message)
        {
            if (Message == null)
                Message = new List<string>();

            if (!string.IsNullOrEmpty(message))
            {
                Message.Add(message);
                StateHasChanged();
            }
        }
    }
}
