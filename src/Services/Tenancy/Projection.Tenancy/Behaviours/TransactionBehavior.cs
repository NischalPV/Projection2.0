using MediatR;
using Microsoft.EntityFrameworkCore;
using Projection.BuildingBlocks.EventBus.Extensions;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.Shared.Commands;
using Projection.Shared.Services.Integration;
using Projection.Tenancy.Infrastructure.Data.Contexts;

namespace Projection.Tenancy.Behaviours;

public class TransactionBehavior<TRequest, TResponse>(TenancyDbContext dbContext,
    IApiIntegrationEventService<IntegrationEventLogContext> integrationEventService,
    ILogger<TransactionBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : BaseCommand<TResponse>
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();

        try
        {
            if (dbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                await using var transaction = await dbContext.BeginTransactionAsync();
                using (logger.BeginScope(new List<KeyValuePair<string, object>>
                {
                    new("TransactionContext", transaction.TransactionId)
                }))
                {
                    logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                    response = await next();

                    logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                    await dbContext.CommitTransactionAsync(transaction);

                    transactionId = transaction.TransactionId;
                }

                await integrationEventService.PublishEventsThroughEventBusAsync(transactionId, typeof(TenancyDbContext).Name);
            });

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}
