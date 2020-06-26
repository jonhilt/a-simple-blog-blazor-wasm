using System.Linq;
using ASimpleBlogStarter.Server.Data;
using ASimpleBlogStarter.Server.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ASimpleBlogStarter.Server.Plumbing
{
    public static class DataSeeder
    {
        public static void SeedTestUsers(IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                var result = roleManager.CreateAsync(new IdentityRole("admin")).Result;
            }

            CreateTestUser(userManager, "admin@jonhilton.net", "admin@jonhilton.net", "SuperLongPassword1$", "admin");
        }

        private static void CreateTestUser(UserManager<ApplicationUser> userManager, string userName, string email, string password, string role = null)
        {
            var existingUser = userManager.FindByEmailAsync(email).Result;
            if (existingUser == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = userName,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(newUser, password).Result;

                if (result.Succeeded && !string.IsNullOrEmpty(role))
                {
                    userManager.AddToRoleAsync(newUser, role).Wait();
                }
            }
        }
    }

    public static class Database
    {
        public static void InitialiseDatabase(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    context.Database.Migrate();

                    if (!context.Posts.Any())
                    {
                        context.Posts.Add(new Post
                        {
                            Body = "<h1>Hello World</h1>",
                            Title = "The best post",
                            Slug = "best"
                        });
                        context.Posts.Add(new Post
                        {
                            Body = "<p>Let's explore how Blazor stacks up in production...</p>",
                            Title = "Is Blazor actually any good for production?",
                            Slug = "blazor-good-for-prod"
                        });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
