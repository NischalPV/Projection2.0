
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.BuildingBlocks.IntegrationEventLogEF.Services;
using Projection.ServiceDefaults;
using Projection.ServiceDefaults.Services.Identity;
using Projection.Shared.Behaviours;
using Projection.Shared.Services.Integration;
using Projection.Tenancy.Behaviours;
using Projection.Tenancy.Infrastructure.Data.Contexts;
using System.Reflection;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var Configuration = builder.Configuration;

        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        // Add the authentication services to DI
        builder.AddDefaultAuthentication();

        builder.Services.AddDbContext<TenancyDbContext>((serviceProvider, options) =>
        {
            //var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            //var claims = httpContextAccessor.HttpContext.User.Claims;
            //var tenancyJson = claims.FirstOrDefault(c => c.Type == "TenancyJson")?.Value;

            //if (string.IsNullOrEmpty(tenancyJson))
            //{
            //    throw new Exception("TenancyJson claim is missing");
            //}

            //var tenancy = JsonConvert.DeserializeObject<TenancySettings>(tenancyJson);

            //var connectionString = tenancy.AccountingDbConnection ?? configuration.GetConnectionString("DefaultConnection");

            System.Diagnostics.Debug.WriteLine("TenancyDbContext::conn ->" + connectionString);

            options.UseNpgsql(connectionString, npgsqlOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            });

            //options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
            //{
            //    sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            //    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //});

        }, ServiceLifetime.Scoped);

        builder.Services.AddDbContext<IntegrationEventLogContext>((serviceProvider, options) =>
        {
            //var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            //var claims = httpContextAccessor.HttpContext.User.Claims;
            //var tenancyJson = claims.FirstOrDefault(c => c.Type == "TenancyJson")?.Value;

            //if (string.IsNullOrEmpty(tenancyJson))
            //{
            //    throw new Exception("TenancyJson claim is missing");
            //}

            //var tenancy = JsonConvert.DeserializeObject<TenancySettings>(tenancyJson);
            //var connectionString = tenancy.AccountingDbConnection ?? configuration.GetConnectionString("DefaultConnection");

            options.UseNpgsql(connectionString, npgsqlOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            });

            //options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
            //{
            //    sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            //    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //});
        });

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Add the integration services that consume the DbContext
        builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<IntegrationEventLogContext>>();

        builder.Services.AddTransient(typeof(IApiIntegrationEventService<>), typeof(ApiIntegrationEventService<>));

        builder.AddRabbitMqEventBus("EventBus", Assembly.GetExecutingAssembly());

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<IIdentityService, IdentityService>();

        // Configure mediatR
        var services = builder.Services;

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        services.AddMediatorValidators();
    }

    private static void AddEventBusSubscriptions(this IEventBusBuilder builder)
    {
    }
}

