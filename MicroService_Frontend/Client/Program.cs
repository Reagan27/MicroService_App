using Blazored.LocalStorage;
using MicroService_Frontend.Services.Auth;
using MicroService_Frontend.Services.AuthProvider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
namespace MicroService_Frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // localstorage registration
            builder.Services.AddBlazoredLocalStorage();


            builder.Services.AddScoped<IAuthInterface, AuthService>();

            // Authprovider configuration
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvideService>();

            await builder.Build().RunAsync();
        }
    }
}