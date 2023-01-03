using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Outbox;
using Newtonsoft.Json;

namespace Infrastructure.Common.DomainEventsDispatching
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IDomainEventsAccessor _domainEventsProvider;
        private readonly DomainEventKafkaMapper _domainEventKafkaMapper;
        private readonly IOutbox _outbox;

        public DomainEventsDispatcher(
            IDomainEventsAccessor domainEventsProvider,
            DomainEventKafkaMapper domainEventKafkaMapper,
            IOutbox outbox)
        {
            _domainEventsProvider = domainEventsProvider;
            _domainEventKafkaMapper = domainEventKafkaMapper;
            _outbox = outbox;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEvents = _domainEventsProvider.GetAllDomainEvents();

            _domainEventsProvider.ClearAllDomainEvents();

            foreach (var outboxMessage in from domainEvent in domainEvents
                     let aggregateId = domainEvent.AggregateId
                     let topicName = _domainEventKafkaMapper.GetName(domainEvent)
                     let payload = JsonConvert.SerializeObject(domainEvent)
                     select new OutboxMessage(aggregateId, topicName, payload, DateTime.Now))
            {
                await _outbox.Add(outboxMessage);
            }
        }
    }
}