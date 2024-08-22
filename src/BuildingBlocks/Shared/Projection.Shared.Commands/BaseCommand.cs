using MediatR;
using System.Runtime.Serialization;

namespace Projection.Shared.Commands;

[DataContract]
public class BaseCommand<R> : IRequest<R>
{
    [DataMember]
    public string Id { get; set; }
}
