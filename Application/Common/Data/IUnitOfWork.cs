using System.Threading;
using System.Threading.Tasks;
using Domain.B2BMasters;
using Domain.PreReservations;

namespace Application.Common.Data
{
    public interface IUnitOfWork
    {
        IB2BMasterRepository MasterRepository { get; }
        
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}