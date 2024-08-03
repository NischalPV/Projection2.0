using Microsoft.EntityFrameworkCore;
using Projection.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Identity.Infrastructure.Data.Contexts;

public class ProjectionOpenIdContext(DbContextOptions<ProjectionOpenIdContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schema.OPENID_SCHEMA);
        builder.UseOpenIddict();
        base.OnModelCreating(builder);

        Masterdata.SeedIdentityServerMasterData(builder);
    }
}
