using Dapper.Contrib.Extensions;
using MapLocationShared.Model;
using System;

namespace DapperRepository.Models
{

    public class LocationGPSDapper 
    {
        #region Construtor
        public LocationGPSDapper()
        {

        }

        public LocationGPSDapper(LocationGPS location)
        {
            Id = location.Id;
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            Altitude = location.Altitude;
            Accuracy = location.Accuracy;
            AltitudeAccuracy = location.AltitudeAccuracy;
            Heading = location.Heading;
            Speed = location.Speed;
            PointDate = location.PointDate;
            IdUser = location.IdUser;
            SessionId = location.SessionId;
        }
        #endregion

        #region Atributos
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
        #endregion

        #region Metodos auxiliares
        public void AddUser()
        {

        }
        #endregion
    }
}
