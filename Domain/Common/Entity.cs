using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Common
{
    public abstract class Entity<TPrimaryKey> : IDomainEntity
        where TPrimaryKey : struct
    {
        public TPrimaryKey Id { get; }
        public bool IsDeleted { get; protected init; }
        public DateTime? CreationDate { get; protected init; }
        public DateTime? LastModifiedDate { get; } = DateTime.Now;

        [IgnoreMember] private readonly List<Func<IDomainEvent>> _domainEventFns = new List<Func<IDomainEvent>>();

        [IgnoreMember]
        public IEnumerable<IDomainEvent> DomainEvents => _domainEventFns.Select(fn => fn.Invoke()).ToList();

        public void ClearDomainEvents()
        {
            _domainEventFns?.Clear();
        }

        public void AddDomainEvent(Func<IDomainEvent> func)
        {
            _domainEventFns.Add(func);
        }

        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }

    public interface IDomainEntity
    {
        IEnumerable<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
        void AddDomainEvent(Func<IDomainEvent> func);
    }
}