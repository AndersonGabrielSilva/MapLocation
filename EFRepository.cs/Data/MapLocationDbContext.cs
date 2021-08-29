using MapLocationShared.Entities;
using MapLocationShared.Utils;
using Microsoft.EntityFrameworkCore;

namespace EFRepository.cs.Data
{
    public class MapLocationDbContext : DbContext
    {

        #region Data Settings
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(DataSettings.ConnectionString);
        #endregion

        #region Entities
        public DbSet<User> User { get; set; }
        public DbSet<Address> Adress { get; set; }
        #endregion
    }
}
