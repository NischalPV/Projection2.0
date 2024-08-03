using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projection.Identity.Domain.Entities;
using Projection.Shared.Constants;

namespace Projection.Identity.Infrastructure.Data.Contexts;

public class ProjectionIdentityContext(DbContextOptions<ProjectionIdentityContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schema.IDENTITY_SCHEMA);
        base.OnModelCreating(builder);

        Masterdata.SeedUsingMigration(builder);
    }
}
