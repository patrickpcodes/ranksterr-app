using Ranksterr.Domain.Users;
using Ranksterr.Infrastructure;

namespace Ranksterr.Api.IntegrationTests;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development"); // or "Test" if you prefer

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.Test.json")
                  .AddEnvironmentVariables();
        });

        builder.ConfigureServices(services =>
        {
            // Remove the app's ApplicationDbContext registration
            // var appDbDescriptor = services.SingleOrDefault(
            //     d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            // if (appDbDescriptor != null)
            // {
            //     services.Remove(appDbDescriptor);
            // }

            // Remove the app's UserDbContext registration
            var userDbDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<UserDbContext>));
            if (userDbDescriptor != null)
            {
                services.Remove(userDbDescriptor);
            }

            // Add ApplicationDbContext using an in-memory database for testing
            // services.AddDbContext<ApplicationDbContext>(options =>
            // {
            //     options.UseInMemoryDatabase("InMemoryAppDbForTesting");
            // });

            // Add UserDbContext using an in-memory database for testing
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryUserDbForTesting");
            });

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database contexts
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                // var appDb = scopedServices.GetRequiredService<ApplicationDbContext>();
                var userDb = scopedServices.GetRequiredService<UserDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                // Ensure the databases are created
                // appDb.Database.EnsureCreated();
                userDb.Database.EnsureCreated();

                try
                {
                    // Apply migrations to ApplicationDbContext
                    // appDb.Database.Migrate();

                    // Apply migrations to UserDbContext
                    userDb.Database.Migrate();

                    // Seed the databases with test data
                    // SeedApplicationData(appDb);
                    SeedUserData(userDb);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the databases. Error: {Message}", ex.Message);
                }
            }
        });
    }

    // private void SeedApplicationData(ApplicationDbContext context)
    // {
    //     // Add seed data for ApplicationDbContext here
    //     //context.YourEntities.Add(new YourEntity { /* ... */ });
    //     context.SaveChanges();
    // }

    private void SeedUserData(UserDbContext context)
    {
        //var user = new ApplicationUser()
        // Add seed data for UserDbContext here
        context.Users.Add(new ApplicationUser());
        context.SaveChanges();
    }
}
