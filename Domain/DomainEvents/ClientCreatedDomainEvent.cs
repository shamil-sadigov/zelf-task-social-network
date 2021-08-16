using Domain.BuildingBlocks;
using Domain.ValueObjects;

namespace Domain.DomainEvents
{
    public record ClientCreatedDomainEvent(ClientId ClientId, ClientName ClientName) : DomainEventBase;
}