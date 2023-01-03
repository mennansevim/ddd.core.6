namespace Domain.Common
{
    public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>
        where TPrimaryKey : struct
    {
    }
}