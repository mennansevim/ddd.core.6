using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Domain.B2BMasters;
using Infrastructure.Common.Data;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Domain.B2BMasters
{
    public class B2BMasterRepository : EfAggregateRootRepositoryBase<B2BMaster, long>, IB2BMasterRepository
    {
        private readonly B2BContext _context;

        public B2BMasterRepository(B2BContext context) : base(context)
        {
            _context = context;
        }

        public async Task<long> GenerateOrderParentId()
        {
            await using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = $"SELECT (NEXT VALUE FOR b2b_parent_id_seq)";
            command.CommandType = CommandType.Text;
            await _context.Database.OpenConnectionAsync();

            await using var result = await command.ExecuteReaderAsync();
            await result.ReadAsync();
            return result.GetInt64(0);
        }

        public async Task<List<B2BMaster>> GetByB2BParentIdAsync(long orderParentId)
        {
            return await _context.B2BMasters.Where(master => master.OrderParentId == orderParentId).ToListAsync();
        }
    }
}