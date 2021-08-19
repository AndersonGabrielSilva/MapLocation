using BrowserInterop.Extensions;
using BrowserInterop.Geolocation;
using MapLocation.Client.Extensions;
using MapLocation.Client.Shared.Component.GeographicLocation;
using MapLocation.Client.Ultis;
using MapLocation.Shared.SignalR;
using MapLocationShared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapLocation.Client.Pages.GeographicLocation
{
    [Authorize]
    public class RealTimeBase : Base, IAsyncDisposable
    {
        #region Atributos
        public Maps Map;

        protected List<LocationGPS> lstLocationGPS { get; set; }
        protected WindowNavigatorGeolocation geolocationWrapper;
        protected GeolocationPosition currentPosition;
        protected List<GeolocationPosition> positioHistory = new List<GeolocationPosition>();
        private IAsyncDisposable geopositionWatcher;

        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public AuthenticationStateProvider authenticationStateProvider { get; set; }

        protected HubConnection hubConnection { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            var window = await JS.Window();
            var navigator = await window.Navigator();

            geolocationWrapper = navigator.Geolocation;
        }

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

                        hubConnection = new HubConnectionBuilder()
                                .WithUrl(url + SignalRName.RouteLocationHub)
                                .Build();
                        var group = "";
                        hubConnection.On<List<LocationGPS>>(group + SignalRName.LocationHub, async (lstLocation) => await ReceveidLocation(lstLocation));

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

        protected async Task GetGeolocation()
        {
            currentPosition = (await geolocationWrapper.GetCurrentPosition(new PositionOptions()
            {
                EnableHighAccuracy = true,
                MaximumAgeTimeSpan = TimeSpan.FromHours(1),
                TimeoutTimeSpan = TimeSpan.FromMinutes(1),
            })).Location;

            SendLocation(currentPosition);
        }

        protected async Task WatchPosition()
        {
            geopositionWatcher = await geolocationWrapper.WatchPosition(async (p) =>
            {
                positioHistory.Add(p.Location);
                SendLocation(p.Location);
                StateHasChanged();
            });
        }

        protected async Task StopWatch()
        {
            if (!Equals(geopositionWatcher, null))
            {
                await geopositionWatcher.DisposeAsync();
                geopositionWatcher = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await StopWatch();
        }

        protected void SendLocation(GeolocationPosition Position)
        {
            var Location = new LocationGPS().CreateLocationGPS(Position);

            string groupName = "";

            if (hubConnection != null)
                hubConnection.InvokeAsync(SignalRName.SaveLocationNotify, groupName, Location);
        }

        protected async Task ReceveidLocation(List<LocationGPS> lstLocationGPS)
        {
            var teste = 0;
        }

    }
}
