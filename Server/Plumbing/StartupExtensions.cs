using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ASimpleBlogStarter.Server.Plumbing
{
    public static class StartupExtensions
    {
        public static void SeedTestUsers(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            DataSeeder.SeedTestUsers(serviceScope);
        }
    }
}
