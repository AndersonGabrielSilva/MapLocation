using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MapLocation.Server.Areas.Identity.IdentityHostingStartup))]
namespace MapLocation.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}