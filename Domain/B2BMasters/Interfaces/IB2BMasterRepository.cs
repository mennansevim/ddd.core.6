using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.B2BMasters
{
    public interface IB2BMasterRepository : IAggregateRootRepository<B2BMaster, long>
    {
        Task<long> GenerateOrderParentId();
        Task<List<B2BMaster>> GetByB2BParentIdAsync(long b2bParentId);
    }
}