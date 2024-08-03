using MassTransit;
using Projection.BuildingBlocks.EventBus.Abstractions;
using Projection.BuildingBlocks.EventBus.Events;

namespace Projection.BuildingBlocks.EventBusRabbitMQ;

public class RabbitMQEventBus(ILogger<RabbitMQEventBus> logger,
                              IPublishEndpoint publishEndpoint) : IEventBus
{
    public async Task PublishAsync(IntegrationEvent @event, Type type)
    {
        if (type is null)
        {
            type = @event.GetType();
        }
        
        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("Publishing event of type {EventType} to RabbitMQ: {EventId}", type, @event.Id);
        }
        await publishEndpoint.Publish(@event, type);
    }
}