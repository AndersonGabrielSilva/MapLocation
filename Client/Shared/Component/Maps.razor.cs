using BlazorGPS.Client.Utils;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorGPS.Client.Shared.Component
{
    public class MapsBase : Base
    {
        #region Properties

        #endregion

        public MapsBase()
        {
            
        }

        #region Events
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await JS.InvokeVoidAsync("InitializeMap", Attribution, MinZoom, MaxZoom, "http://{s}.tile.osm.org/{z}/{x}/{y}.png");
        }
        #endregion
    }
}
