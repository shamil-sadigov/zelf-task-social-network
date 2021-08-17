#region

using System;
using MediatR;

#endregion

namespace Domain.BuildingBlocks
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }

        DateTime OccurredOn { get; }
    }
}