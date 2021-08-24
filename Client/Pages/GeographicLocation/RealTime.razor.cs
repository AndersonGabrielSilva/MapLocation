using BlazorGPS.Client.Extensions;
using BlazorGPS.Client.Shared.Component.GeographicLocation;
using BlazorGPS.Client.Utils;
using BrowserInterop.Extensions;
using BrowserInterop.Geolocation;
using MapLocation.Shared.SignalR;
using MapLocationShared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorGPS.Client.Pages.GeographicLocation
{
    public class RealTimeBase : Base, IAsyncDisposable
    {
        #region Inject
        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        protected ISnackbar Snackbar { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }
        #endregion

        #region Interface
        private IAsyncDisposable geopositionWatcher;
        #endregion 

        #region Properties

        #region String
        private string typeMaps;

        public string TypeMaps
        {
            get => typeMaps;
            set
            {
                typeMaps = value;
            }
        }
        #endregion

        #region Object
        public Maps Map { get; set; }
        protected HubConnection hubConnection { get; set; }
        protected GeolocationPosition currentPosition { get; set; }
        protected WindowNavigatorGeolocation geolocationWrapper { get; set; }
        #endregion

        #region List
        protected List<LocationGPS> lstLocationGPS { get; set; }
        protected List<GeolocationPosition> positioHistory { get; set; }
        #endregion

        #endregion

        #region Constructor
        public RealTimeBase()
        {
            positioHistory = new List<GeolocationPosition>();
            typeMaps = "http://{s}.tile.osm.org/{z}/{x}/{y}.png";
        }
        #endregion

        #region Events Base
        protected override async Task OnInitializedAsync()
        {
            var window = await JS.Window();
            var navigator = await window.Navigator();

            geolocationWrapper = navigator.Geolocation;

            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            Snackbar.Configuration.MaxDisplayedSnackbars = 10;
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
        #endregion

        #region Events
        protected async Task GetGeolocation()
        {
            currentPosition = (await geolocationWrapper.GetCurrentPosition(new PositionOptions()
            {
                EnableHighAccuracy = true,
                MaximumAgeTimeSpan = TimeSpan.FromHours(1),
                TimeoutTimeSpan = TimeSpan.FromMinutes(1),
            })).Location;

            if (!Equals(currentPosition, null))
            {
                var SB = new StringBuilder();

                SB.Append($"<ul>");
                SB.Append($"  <li>");
                SB.Append($"    Latitude: {currentPosition.Coords.Latitude}");
                SB.Append($"  </li>");
                SB.Append($"  <li>");
                SB.Append($"    Longitude: {currentPosition.Coords.Longitude}");
                SB.Append($"  </li>");
                SB.Append($"  <li>");
                SB.Append($"    Altitude: {currentPosition.Coords.Altitude}");
                SB.Append($"  </li>");
                SB.Append($"  <li>");
                SB.Append($"    Accuracy: {currentPosition.Coords.Accuracy}");
                SB.Append($"  </li>");
                SB.Append($"</ul>");

                Snackbar.Add(SB.ToString(), Severity.Info);

                await Marker(currentPosition.Coords.Latitude,
                             currentPosition.Coords.Longitude,
                             currentPosition.Coords.Altitude,
                             currentPosition.Coords.Accuracy);
            }

            SendLocation(currentPosition);
        }

        protected async Task WatchPosition()
        {
            geopositionWatcher = await geolocationWrapper.WatchPosition(async (p) =>
            {
                //positioHistory.Add(p.Location);

                //var SB = new StringBuilder();

                //SB.Append($"<ul>");
                //SB.Append($"  <li>");
                //SB.Append($"    Latitude: {p.Location.Coords.Latitude}");
                //SB.Append($"  </li>");
                //SB.Append($"  <li>");
                //SB.Append($"    Longitude: {p.Location.Coords.Longitude}");
                //SB.Append($"  </li>");
                //SB.Append($"  <li>");
                //SB.Append($"    Altitude: {p.Location.Coords.Altitude}");
                //SB.Append($"  </li>");
                //SB.Append($"  <li>");
                //SB.Append($"    Accuracy: {p.Location.Coords.Accuracy}");
                //SB.Append($"  </li>");
                //SB.Append($"</ul>");

                //Snackbar.Add(SB.ToString(), Severity.Info);

                await Marker(p.Location.Coords.Latitude,
                             p.Location.Coords.Longitude,
                             p.Location.Coords.Altitude,
                             p.Location.Coords.Accuracy);

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

        public async Task Marker(double latitude, double longitude, double? altitude, double accuracy) =>
            await JS.InvokeVoidAsync("Marker", latitude, longitude, altitude, accuracy);

        public async ValueTask DisposeAsync() =>
            await StopWatch();

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

        public async Task RemoverMarcacoes() =>
            await JS.InvokeVoidAsync("RemoverMarcacaoMapa");

        public async void SetLayout() =>
            await JS.InvokeVoidAsync("Reset", Attribution, MinZoom, MaxZoom, TypeMaps);

        protected async void GetLayout()
        {
            var ParametersDialog = new DialogParameters();
            ParametersDialog.Add("TypeMapsDialog", typeMaps);

            var OptionDialog = new DialogOptions();
            OptionDialog.MaxWidth = MaxWidth.Large;
            OptionDialog.CloseButton = false;
            OptionDialog.DisableBackdropClick = true;
            OptionDialog.Position = DialogPosition.Center;

            var Dialog = DialogService.Show<LayoutDialog>($"Layouts", ParametersDialog, OptionDialog);
            var Result = await Dialog.Result;

            if (!Result.Cancelled)
            {
                TypeMaps = (string)Result.Data;
                SetLayout();
            }

            StateHasChanged();
        }
        #endregion
    }
}
