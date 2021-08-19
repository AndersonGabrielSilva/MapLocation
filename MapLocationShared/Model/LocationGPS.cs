using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace MapLocationShared.Model
{
    public class LocationGPS
    {    
        [Key]
        public virtual int Id { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
        public virtual double? Altitude { get; set; }
        public virtual double Accuracy { get; set; }
        public virtual double? AltitudeAccuracy { get; set; }
        public virtual double? Heading { get; set; }
        public virtual double? Speed { get; set; }
        public virtual DateTime PointDate { get; set; }
        public virtual string IdUser { get; set; }
        public virtual int SessionId { get; set; }
    }
}
