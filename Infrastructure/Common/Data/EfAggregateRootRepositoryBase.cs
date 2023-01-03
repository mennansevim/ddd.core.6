using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Application.Common.Exceptions;

namespace Infrastructure.Common.Data
{
    public class
        EfAggregateRootRepositoryBase<TAggregateRoot, TPrimaryKey> : IAggregateRootRepository<TAggregateRoot,
            TPrimaryKey>
        where TPrimaryKey : struct
        where TAggregateRoot : AggregateRoot<TPrimaryKey>
    {
        private readonly DbSet<TAggregateRoot> _entities;

        protected EfAggregateRootRepositoryBase(DbContext context)
        {
            _entities = context.Set<TAggregateRoot>();
        }

        public async Task AddAsync(TAggregateRoot instance)
        {
            await _entities.AddAsync(instance);
        }

        public async Task DeleteByIdAsync(TPrimaryKey id)
        {
            var entity = await GetByIdAsync(id);
            _entities.Remove(entity);
        }

        public async Task<TAggregateRoot> GetByIdAsync(TPrimaryKey id)
        {
            return await _entities.AsTracking().FirstOrDefaultAsync(x => x.Id.Equals(id))
                   ?? throw new NotFoundException(nameof(TAggregateRoot), id);
        }
    }
}