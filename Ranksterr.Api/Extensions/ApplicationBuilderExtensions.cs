﻿using Microsoft.EntityFrameworkCore;
using Ranksterr.Infrastructure;
using Ranksterr.Infrastructure.Migrations.App;

namespace Ranksterr.Api.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations( this IApplicationBuilder app )
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();

        using UserDbContext userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        userDbContext.Database.Migrate();
    }

    // public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    // {
    //     app.UseMiddleware<ExceptionHandlingMiddleware>();
    // }
    //
    // public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    // {
    //     app.UseMiddleware<RequestContextLoggingMiddleware>();
    //
    //     return app;
    // }
}