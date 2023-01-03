using System;
using Application.Common.Data;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.B2BMasters;
using Domain.PreReservations;
using Infrastructure.Common.DomainEventsDispatching;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        private readonly B2BContext _context;

        public UnitOfWork(IB2BMasterRepository b2BMasterRepository,
            IDomainEventsDispatcher domainEventsDispatcher,
            B2BContext context)
        {
            _domainEventsDispatcher = domainEventsDispatcher;
            _context = context;

            MasterRepository = b2BMasterRepository;
        }

        public IB2BMasterRepository MasterRepository { get; } 

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await _domainEventsDispatcher.DispatchEventsAsync();
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new UnitOfWorkException("While saving db changes error occurred!",e);
            }
        }
    }
}