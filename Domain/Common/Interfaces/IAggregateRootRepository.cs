using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IAggregateRootRepository<TEntity, in TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : AggregateRoot<TPrimaryKey>
    {
        Task AddAsync(TEntity instance);

        Task<TEntity> GetByIdAsync(TPrimaryKey id);

        Task DeleteByIdAsync(TPrimaryKey id);
    }
}