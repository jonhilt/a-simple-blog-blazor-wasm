using ASimpleBlogStarter.Client.Shared;
using ASimpleBlogStarter.Shared.Post;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ASimpleBlogStarter.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("ASimpleBlogStarter.AnonymousAPI",
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddHttpClient("ASimpleBlogStarter.ServerAPI",
                    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddTransient(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("ASimpleBlogStarter.ServerAPI"));

            builder.Services.AddApiAuthorization();

            builder.Services.AddTransient<ISlugValidator, SlugValidator>();

            await builder.Build().RunAsync();
        }
    }
}