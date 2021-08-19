using Microsoft.Data.SqlClient;
using System;

namespace MapLocationShared.Utils
{
    public static class DataSettings
    {
        public static string ConnectionString { get; set; }

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = null;

            if (!string.IsNullOrEmpty(ConnectionString))
                connection =  new SqlConnection(ConnectionString);

            return connection;
        }
    }
}
