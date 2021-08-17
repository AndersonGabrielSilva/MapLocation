using MapLocation.Shared.SignalR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapLocation.Client.Pages
{
    public class IndexBase : ComponentBase
    {
        #region Atributos
        [Inject]
        public IConfiguration Configuration { get; set; }
        #endregion

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    if (firstRender)
                    {
                        #region SignalR
                        
                        /***************LEMBRAR*****************/
                        // Daniel ao realizar o login precisa configurar o authenticationStateProcider


                        //var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
                        //var group = authenticationState.User.Claims.SingleOrDefault(x => x.Type == nameof(SignalRName.Group)).Value.ToString();

                        var url = Configuration.GetSection("ApiServiceHub").GetSection("ApiUrl").Value;

                        HubConnection hubConnection;
                        hubConnection = new HubConnectionBuilder()
                                .WithUrl(url + SignalRName.RouteLocationHub)
                                .Build();

                        //hubConnection.On(group + SignalRName.LocationHub, async () => await TesteHub());

                        await hubConnection.StartAsync();
                        #endregion
                    }
                }
                catch (Exception erro)
                {
                    var msgErro = erro.Message;
                }
            }
        }

        private Task TesteHub() 
        {
            Console.WriteLine("Teste");

            return Task.FromResult("Teste");
        }
    }
}
