namespace Projection.Tenancy.Features.Apis;

public static class TenantsApi
{
    public static RouteGroupBuilder MapTenantsApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/v{version:apiVersion}/tenants")
            .HasApiVersion(1.0);

        api.MapGet("/", GetTenantsAsync);

        return api;
    }

    public static async Task GetTenantsAsync(HttpContext context)
    {
        var tenants = new[]
        {
            new { Id = 1, Name = "Tenant 1" },
            new { Id = 2, Name = "Tenant 2" },
            new { Id = 3, Name = "Tenant 3" },
        };

        await context.Response.WriteAsJsonAsync(tenants);
    }
}
