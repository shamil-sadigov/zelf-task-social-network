using System;

namespace Domain.BuildingBlocks
{
    public record DomainEventBase : IDomainEvent
    {
        protected DomainEventBase()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }

        public Guid Id { get; }

        public DateTime OccurredOn { get; }
    }
}