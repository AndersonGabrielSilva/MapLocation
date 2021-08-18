using BrowserInterop.Extensions;
using BrowserInterop.Geolocation;
using MapLocation.Client.Shared.Component.GeographicLocation;
using MapLocation.Client.Ultis;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapLocation.Client.Pages.GeographicLocation
{
    [Authorize]
    public class RealTimeBase : Base, IAsyncDisposable
    {
        public Maps Map;

        protected WindowNavigatorGeolocation geolocationWrapper;
        protected GeolocationPosition currentPosition;
        protected List<GeolocationPosition> positioHistory = new List<GeolocationPosition>();
        private IAsyncDisposable geopositionWatcher;

        protected override async Task OnInitializedAsync()
        {
            var window = await JS.Window();
            var navigator = await window.Navigator();

            geolocationWrapper = navigator.Geolocation;
        }

        protected async Task GetGeolocation()
        {
            currentPosition = (await geolocationWrapper.GetCurrentPosition(new PositionOptions()
            {
                EnableHighAccuracy = true,
                MaximumAgeTimeSpan = TimeSpan.FromHours(1),
                TimeoutTimeSpan = TimeSpan.FromMinutes(1),
            })).Location;
        }

        protected async Task WatchPosition()
        {
            geopositionWatcher = await geolocationWrapper.WatchPosition(async (p) =>
            {
                positioHistory.Add(p.Location);
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

    }
}
