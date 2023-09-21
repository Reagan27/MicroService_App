using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.Text.Json;

namespace MicroService_Frontend.Services.AuthProvider
{
    public class CustomAuthProvideService:AuthenticationStateProvider
    {
        private ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        public CustomAuthProvideService(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {   

            //read Token from Local storage 
            string authToken = await _localStorage.GetItemAsStringAsync("authToken");

            var identity = new ClaimsIdentity();

            _httpClient.DefaultRequestHeaders.Authorization = null;
            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                }
                catch (Exception ex)
                {
                    await _localStorage.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;

        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}