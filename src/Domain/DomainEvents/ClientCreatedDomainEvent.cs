#region

using Domain.BuildingBlocks;
using Domain.ValueObjects;

#endregion

namespace Domain.DomainEvents
{
    public record ClientCreatedDomainEvent(ClientId ClientId, ClientName ClientName) : DomainEventBase;
}