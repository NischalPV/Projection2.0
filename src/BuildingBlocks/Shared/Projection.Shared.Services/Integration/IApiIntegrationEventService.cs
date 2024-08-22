using Projection.BuildingBlocks.EventBus.Events;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.Shared.Data.Contexts;

namespace Projection.Shared.Services.Integration;

public interface IApiIntegrationEventService<TContext> where TContext : BaseDbContext
{
    Task AddAndSaveEventAsync(IntegrationEvent evt);
    Task PublishEventsThroughEventBusAsync(Guid transactionId, string appName);
    Task<IntegrationEventLogEntry> GetIntegrationEventLogEntryAsync(Guid EventId);
}
