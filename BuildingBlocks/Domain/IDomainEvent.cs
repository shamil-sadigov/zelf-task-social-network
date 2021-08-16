using System;
using MediatR;

namespace Domain.BuildingBlocks.BuildingBlocks
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }

        DateTime OccurredOn { get; }
    }
}