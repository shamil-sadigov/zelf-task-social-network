using System;
using MediatR;

namespace Domain.BuildingBlocks
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }

        DateTime OccurredOn { get; }
    }
}