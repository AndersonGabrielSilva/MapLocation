
using BrowserInterop.Geolocation;
using MapLocationShared.Model;
using System;
using System.Collections.Generic;

namespace BlazorGPS.Client.Extensions
{
    public static class Extensions
    {
        public static List<LayoutList> CreateLayoutList(this List<LayoutList> List)
        {
            List.Add(new LayoutList("http://{s}.tile.osm.org/{z}/{x}/{y}.png", "Default"));
            List.Add(new LayoutList("https://api.maptiler.com/maps/hybrid/256/{z}/{x}/{y}@2x.jpg?key=OhKLq5wlAdK90y0vDvPY", "Maptiler - Satellite Hybrid"));
            List.Add(new LayoutList("https://tile.thunderforest.com/cycle/{z}/{x}/{y}.png?apikey=3b495e9cd83e42f18f72300d39d88d8c", "Thunderforest - OpenCycleMap"));
            List.Add(new LayoutList("https://tile.thunderforest.com/transport/{z}/{x}/{y}.png?apikey=3b495e9cd83e42f18f72300d39d88d8c", "Thunderforest - Transport"));
            List.Add(new LayoutList("https://tile.thunderforest.com/landscape/{z}/{x}/{y}.png?apikey=3b495e9cd83e42f18f72300d39d88d8c", "Thunderforest - Landscape"));
            List.Add(new LayoutList("https://tile.thunderforest.com/outdoors/{z}/{x}/{y}.png?apikey=3b495e9cd83e42f18f72300d39d88d8c", "Thunderforest - Outdoors"));
            List.Add(new LayoutList("https://tile.thunderforest.com/atlas/{z}/{x}/{y}.png?apikey=3b495e9cd83e42f18f72300d39d88d8c", "Thunderforest - Atlas"));
            List.Add(new LayoutList("https://tile.thunderforest.com/spinal-map/{z}/{x}/{y}.png?apikey=3b495e9cd83e42f18f72300d39d88d8c", "Thunderforest - Spinal Map"));
            List.Add(new LayoutList("https://tile.thunderforest.com/transport-dark/{z}/{x}/{y}.png?apikey=3b495e9cd83e42f18f72300d39d88d8c", "Thunderforest - Transport Dark"));
            List.Add(new LayoutList("https://tile.thunderforest.com/pioneer/{z}/{x}/{y}.png?apikey=3b495e9cd83e42f18f72300d39d88d8c", "Thunderforest - Pioneer"));
            List.Add(new LayoutList("https://tile.thunderforest.com/neighbourhood/{z}/{x}/{y}.png?apikey=3b495e9cd83e42f18f72300d39d88d8c", "Thunderforest - Neighbourhood"));
            List.Add(new LayoutList("https://tile.thunderforest.com/mobile-atlas/{z}/{x}/{y}.png?apikey=3b495e9cd83e42f18f72300d39d88d8c", "Thunderforest - Mobile Atlas"));

            return List;
        }
        public static LocationGPS CreateLocationGPS(this LocationGPS Location, GeolocationPosition Position)
        {
            if (Location == null)
                Location = new LocationGPS();

            Location.Altitude = Position.Coords.Altitude;
            Location.Latitude = Position.Coords.Latitude;
            Location.Longitude = Position.Coords.Longitude;

            Location.Accuracy = Position.Coords.Accuracy;
            Location.AltitudeAccuracy = Position.Coords.AltitudeAccuracy;

            Location.Heading = Position.Coords.Heading;
            Location.Speed = Position.Coords.Speed;

            Location.PointDate = DateTime.Now;

            Location.SessionId = 0;
            Location.IdUser = "0";

            return Location;
        }

    }
}
