using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Infrastructure.Persistence;

namespace Infrastructure.Common.DomainEventsDispatching
{
    public class DomainEventsAccessor : IDomainEventsAccessor
    {
        private readonly B2BContext _dbContext;

        public DomainEventsAccessor(B2BContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<IDomainEvent> GetAllDomainEvents()
        {
           return _dbContext.ChangeTracker
                .Entries<IDomainEntity>()
                .Where(x => x.Entity.DomainEvents?.Any() == true)
                .SelectMany(x=> x.Entity.DomainEvents)
                .ToList();
        }

        public void ClearAllDomainEvents()
        {
            var domainEntities = _dbContext.ChangeTracker
                .Entries<IDomainEntity>()
                .Where(x => x.Entity.DomainEvents?.Any() == true)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }
}