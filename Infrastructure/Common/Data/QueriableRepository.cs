using Application.Common.Data;
using Infrastructure.Persistence;

namespace Infrastructure.Common.Data
{
    public class QueryableRepository : IQueryableRepository
    {
        private readonly B2BContext _context;

        public QueryableRepository(B2BContext context)
        {
            _context = context;
        }

    }
}