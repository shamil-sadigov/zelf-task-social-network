#region

using System.Collections.Generic;
using Domain.BuildingBlocks;

#endregion

namespace Application.Contracts
{
    public interface IDomainEventAccessor
    {
        IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();
        void ClearAllDomainEvents();
    }
}