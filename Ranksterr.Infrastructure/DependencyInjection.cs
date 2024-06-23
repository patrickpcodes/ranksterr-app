using Asp.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ranksterr.Application.Clock;
using Ranksterr.Domain.Abstractions;
using Ranksterr.Domain.Settings;
using Ranksterr.Infrastructure.Clock;

namespace Ranksterr.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
       AddSettings(services, configuration);
        //AddApiVersioning(services);
        
        return services;
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