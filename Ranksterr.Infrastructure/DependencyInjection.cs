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
using Ranksterr.Infrastructure.Migrations.App;
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

        services.AddAuthorization(options =>{
            options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));

            //TODO can require Role directly, don't need to use claims to handle rolls
            //Need to test this out
            //Can have separate policies for claims, like if people had separate data in their claims
            //Like employee ID, or TITLE, or something else that we save
            //Also can have just a claim or role, not necessarily a specific claim or role
            options.AddPolicy( ClaimPolicyIsAdmin, policy =>
                policy.RequireClaim( ClaimAccessLevelType, ClaimAccessLevelAdmin ) );
        } );
        
        
        services.AddDbContext<ApplicationDbContext>( options => options.UseSqlServer( connectionString ) );
        
        services.AddScoped<IUnitOfWork>( sp => sp.GetRequiredService<ApplicationDbContext>() );
        
        
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