using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MapLocationShared.Model
{
    public class LocationGPS
    {
        [Key]
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
        public double Accuracy { get; set; }
        public double? AltitudeAccuracy { get; set; }
        public double? Heading { get; set; }
        public double? Speed { get; set; }
        public DateTime PointDate { get; set; }
        public string IdUser { get; set; }
        public int SessionId { get; set; }
    }
}
