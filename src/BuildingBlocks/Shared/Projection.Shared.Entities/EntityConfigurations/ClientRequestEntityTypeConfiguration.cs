using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projection.Shared.Constants;
using Projection.Shared.Models;

namespace Projection.Shared.Entities.EntityConfigurations;

public class ClientRequestEntityTypeConfiguration
    : IEntityTypeConfiguration<ClientRequest>
{
    public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
    {
        requestConfiguration.ToTable("Requests", Schema.IDEMPOTENCY_SCHEMA);
    }
}
