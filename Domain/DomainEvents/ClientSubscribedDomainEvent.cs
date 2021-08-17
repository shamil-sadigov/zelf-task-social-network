#region

using Domain.BuildingBlocks;
using Domain.ValueObjects;

#endregion

namespace Domain.DomainEvents
{
    public record ClientSubscribedDomainEvent(
            ClientId SubscriberId,
            ClientId ClientId,
            ClientPopularity ClientPopularity)
        : DomainEventBase;
}