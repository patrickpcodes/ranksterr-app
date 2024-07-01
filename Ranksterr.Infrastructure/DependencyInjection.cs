using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ranksterr.Application.Abstractions;
using Ranksterr.Application.Clock;
using Ranksterr.Domain.Abstractions;
using Ranksterr.Domain.Settings;
using Ranksterr.Domain.Users;
using Ranksterr.Infrastructure.Clock;
using static Ranksterr.Infrastructure.AuthorizationConstants;

namespace Ranksterr.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
       
        AddPersistence(services, configuration);
        
        AddSettings(services, configuration);
        //AddApiVersioning(services);
       
        return services;
    }

    private static void AddPersistence( IServiceCollection services, IConfiguration configuration )
    {
        string connectionString = configuration.GetConnectionString( "Database" ) ??
                                  throw new ArgumentNullException( nameof(configuration) );
    
       
        services.AddDbContext<UserDbContext>( options => options.UseSqlServer( connectionString ) ); 
        
        services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<UserDbContext>()
               .AddDefaultTokenProviders();
        
        // services.AddDbContext<ApplicationDbContext>( options => options.UseSqlServer( connectionString ) );
        //
        // services.AddScoped<IUnitOfWork>( sp => sp.GetRequiredService<ApplicationDbContext>() );
        
    }
    private static void AddSettings(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TmdbSettings>(configuration.GetSection("TMDB"));

        //services.AddSingleton<ISettingsService<TmdbSettings>, TmdbSettingsService>();
        services.AddSingleton<ISettingsService<TmdbSettings>, SettingsService<TmdbSettings>>();
    }
    private static void AddApiVersioning(IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}