using System.Collections.Generic;
using System.Linq;
using Application;
using Application.Contracts;
using Domain.BuildingBlocks;
using MoreLinq.Extensions;

namespace Infrastructure.Database.Implementations
{
    public class DomainEventsAccessor : IDomainEventAccessor
    {
        private readonly ApplicationContext _context;

        public DomainEventsAccessor(ApplicationContext context)
        {
            _context = context;
        }

        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents() =>
            _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.HasDomainEvents)
                .SelectMany(x => x.Entity.DomainEvents!)
                .ToList();

        public void ClearAllDomainEvents() =>
            _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.HasDomainEvents)
                .ForEach(entity => entity.Entity.ClearDomainEvents());
    }
}