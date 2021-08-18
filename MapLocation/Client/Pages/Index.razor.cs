using MapLocation.Shared.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MapLocation.Client.Pages
{
    [Authorize]
    public class IndexBase : ComponentBase
    {

        #region Atributos
        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public AuthenticationStateProvider authenticationStateProvider { get; set; }
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
                        var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
                        var group = authenticationState.User.Claims.SingleOrDefault(x => x.Type == nameof(SignalRName.Group)).Value.ToString();

                        var url = Configuration.GetSection("ApiServiceHub").GetSection("ApiUrl").Value;

                        HubConnection hubConnection;
                        hubConnection = new HubConnectionBuilder()
                                .WithUrl(url + SignalRName.LocationHub)
                                .Build();

                        hubConnection.On<string>(group + SignalRName.LocationHub, async (mensagem) => await TesteHub(mensagem));

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

        private async Task TesteHub(string mensagem)
        {
            Console.Write(mensagem);
            return;
        }
    }
}
