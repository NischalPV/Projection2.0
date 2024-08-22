using Projection.Shared.Data.Idempotency;
using Projection.Shared.Data.Repositories;
using Projection.Tenancy.Infrastructure.Data.Contexts;

public static class ServiceRegistry
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseEntityAsyncRepository<,,>), typeof(BaseEntityEfRepository<,,>));

        services.AddScoped<IRequestManager>(services =>
        {
            var context = services.GetRequiredService<TenancyDbContext>();
            return new RequestManager<TenancyDbContext>(context);
        });

        return services;
    }

    public static IServiceCollection AddTransientServices(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddSingletonServices(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddMediatorValidators(this IServiceCollection services)
    {
        //services.AddSingleton<IValidator<AccountCreateCommand>, AccountCreateCommandValidator>();
        //services.AddSingleton<IValidator<AccountUpdateCommand>, AccountUpdateCommandValidator>();

        //services.AddSingleton<IValidator<IdentifiedCommand<AccountCreateCommand, Account>>, IdentifiedCommandValidator<AccountCreateCommand, Account>>();
        return services;
    }
}
