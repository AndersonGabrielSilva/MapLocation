using MapLocation.Client.Auth;
using MapLocation.Shared.Sistema;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MapLocation.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("MapLocation.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MapLocation.ServerAPI"));

            builder.Services.AddApiAuthorization();
            builder.Services.AddScoped<ParametrosClient>();

            builder.Services.AddScoped<TokenAuthStateProvider>();

            builder.Services.AddScoped<IAuthorizeService, TokenAuthStateProvider>(
                provider => provider.GetRequiredService<TokenAuthStateProvider>());

            builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>(
                provider => provider.GetRequiredService<TokenAuthStateProvider>());

            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
