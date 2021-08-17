using MapLocation.Client.Extensions;
using MapLocation.Shared.Sistema;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace MapLocation.Client.Auth
{
    public class TokenAuthStateProvider : AuthenticationStateProvider, IAuthorizeService
    {
        private readonly IJSRuntime js;
        private HttpClient http;
        private ParametrosClient parametros;
        public static readonly string tokenKey = nameof(tokenKey);


        public TokenAuthStateProvider(IJSRuntime js, HttpClient http, ParametrosClient parametros)
        {
            this.js = js;
            this.http = http;
            this.parametros = parametros;
        }

        private AuthenticationState NaoAutenticado =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        private async Task<AuthenticationState> AutenticarUsuario(string token)
        {
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            try
            {
                parametros.SetInfos(JsonSerializer.Deserialize<ParametrosClient>(await http.GetStringAsync("api/account/getparametrosclient")
                , new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
            }
            catch { parametros.ClearInfos(); return NaoAutenticado; }
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(tokenKey);

            if (string.IsNullOrEmpty(token))
                return NaoAutenticado;

            return await AutenticarUsuario(token);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(string token)
        {
            await js.SetInLocalStorage(tokenKey, token);

            var authState = AutenticarUsuario(token);

            NotifyAuthenticationStateChanged(Task.FromResult(await authState));
        }

        public async Task Logout()
        {
            await js.RemoveItem(tokenKey);

            http.DefaultRequestHeaders.Authorization = null;

            parametros.ClearInfos();

            NotifyAuthenticationStateChanged(Task.FromResult(NaoAutenticado));
        }
    }
}
