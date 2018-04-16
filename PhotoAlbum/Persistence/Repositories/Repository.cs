namespace Persistence.Repositories
{
    using Persistence.Entities;
    using System;
    using System.Data.Entity;

    abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected DbSet<TEntity> Set { get; }

        protected Repository(PhotoAlbumDbContext context) => Set = context.Set<TEntity>();

        public TEntity Get(Guid id) => Set.Find(id);
    }
}
