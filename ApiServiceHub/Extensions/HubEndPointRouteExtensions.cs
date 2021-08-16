using ApiServiceHub.Hubs;
using MapLocation.Shared.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace ApiServiceHub.Extensions
{
    public static class HubEndPointRouteExtensions
    {
        public static void MapHubRoute(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<LocationHub>(SignalRName.RouteLocationHub);
        }
    }
}
