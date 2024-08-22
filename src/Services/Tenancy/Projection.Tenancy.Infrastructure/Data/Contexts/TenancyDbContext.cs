using MediatR;
using Microsoft.EntityFrameworkCore;
using Projection.Shared.Constants;
using Projection.Shared.Data.Contexts;
using Projection.Shared.Data.Extensions;

namespace Projection.Tenancy.Infrastructure.Data.Contexts;

public class TenancyDbContext : BaseDbContext
{
    #region Properties
    internal readonly IMediator? _mediator;
    #endregion

    #region ctors
    public TenancyDbContext(DbContextOptions<TenancyDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        System.Diagnostics.Debug.WriteLine("TenancyDbContext::ctor ->" + this.GetHashCode());
    }

    public TenancyDbContext(DbContextOptions<TenancyDbContext> options) : base(options)
    {
        System.Diagnostics.Debug.WriteLine("TenancyDbContext::ctor ->" + this.GetHashCode());
    }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.TENANCY_SCHEMA);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public override async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}
