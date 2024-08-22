using MediatR;

namespace Projection.Shared.Commands;

public class IdentifiedCommand<TCommand, TResponse>(TCommand command, Guid id) : IRequest<TResponse> where TCommand : IRequest<TResponse>
{
    public TCommand Command { get; } = command;
    public Guid Id { get; } = id;
}
