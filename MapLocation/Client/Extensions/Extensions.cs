using BrowserInterop.Geolocation;
using MapLocationShared.Model;
using System;

namespace MapLocation.Client.Extensions
{
    public static class Extensions
    {

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
