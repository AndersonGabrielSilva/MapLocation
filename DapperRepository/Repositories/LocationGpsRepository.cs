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
    }
}
