using Microsoft.EntityFrameworkCore;
using System.Settings;

namespace EFDataConfiguration
{
    public class MapLocationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(DataBaseSettigns.ConnectionString);
    }
}
