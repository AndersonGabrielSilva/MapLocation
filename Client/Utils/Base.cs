﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http;

namespace BlazorGPS.Client.Utils
{
    public class Base : ComponentBase
    {
        #region Inject
        [Inject]
        public IJSRuntime JS { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }
        #endregion

        #region Constructor
        public Base()
        {
            //this.Template = "https://api.maptiler.com/maps/hybrid/256/{z}/{x}/{y}@2x.jpg?key=OhKLq5wlAdK90y0vDvPY";
            this.Template = "http://{s}.tile.osm.org/{z}/{x}/{y}.png";
            this.Attribution = "Gerado por <a href=\"/\">MapLocation</a>";
            this.MinZoom = 1;
            this.MaxZoom = 50;
        }
        #endregion

        #region Properties
        public string Template { get; set; }

        public string Attribution { get; set; }

        public int MinZoom { get; set; }

        public int MaxZoom { get; set; }

        protected HubConnection hubConnection { get; set; }
        #endregion

        #region URL's

        #endregion

    }
}