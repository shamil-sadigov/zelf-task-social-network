using System.Collections.Generic;
using Domain.BuildingBlocks;

namespace Application.Contracts
{
    public interface IDomainEventAccessor
    {
        IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();
        void ClearAllDomainEvents();
    }
}