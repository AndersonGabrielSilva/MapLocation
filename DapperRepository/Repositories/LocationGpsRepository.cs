using System.Collections.Generic;
using System.Linq;
using Dapper;
using DapperRepository.Models;
using Microsoft.Data.SqlClient;

namespace DapperRepository.Repositories
{
    public class LocationGpsRepository : Repository<LocationGPSDapper>
    {
        private readonly SqlConnection _connection;

        public LocationGpsRepository(SqlConnection connection) : base(connection)
            => _connection = connection;

        public List<LocationGPSDapper> ReadWithRole()
        {
            var query = @"
                SELECT
                    [User].*,
                    [Role].*
                FROM
                    [User]
                    LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                    LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]";

            //var location = new List<LocationGPSDapper>();
            //var items = _connection.Query<LocationGPSDapper, Role, LocationGPSDapper>(
            //    query,
            //    (user, role) =>
            //    {
            //        var usr = location.FirstOrDefault(x => x.Id == user.Id);
            //        if (usr == null)
            //        {
            //            usr = user;

            //            usr.AddRoule(role);

            //            location.Add(usr);
            //        }
            //        else
            //            usr.AddRoule(role);

            //        return user;
            //    }, splitOn: "Id");

            return null;
            //return location;
        }

        public override void Create(LocationGPSDapper model)
        {
            if (_connection == null)
                return;

            #region Insert SQL
            var insertSql = @"INSERT INTO [dbo].[LocationGPS]
                                   ([Latitude]
                                   ,[Longitude]
                                   ,[Altitude]
                                   ,[Accuracy]
                                   ,[AltitudeAccuracy]
                                   ,[Heading]
                                   ,[Speed]
                                   ,[PointDate]
                                   ,[IdUser]
                                   ,[SessionId])
                             VALUES
                                   (@Latitude
                                   ,@Longitude
                                   ,@Altitude
                                   ,@Accuracy
                                   ,@AltitudeAccuracy
                                   ,@Heading
                                   ,@Speed
                                   ,@PointDate
                                   ,@IdUser
                                   ,@SessionId)";
            #endregion

            var rows = _connection.Execute(insertSql, new
            {
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Altitude = model.Altitude,
                Accuracy = model.Accuracy,
                AltitudeAccuracy = model.AltitudeAccuracy,
                Heading = model.Heading,
                Speed = model.Speed,
                PointDate = model.PointDate,
                IdUser = model.IdUser,
                SessionId = model.SessionId
            });

        }
    }
}
