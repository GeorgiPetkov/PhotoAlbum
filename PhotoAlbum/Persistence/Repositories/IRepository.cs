namespace Persistence.Repositories
{
    using System;

    public interface IRepository<TEntity>
    {
        TEntity Get(Guid id);
    }
}
