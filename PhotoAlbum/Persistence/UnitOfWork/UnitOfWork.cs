namespace Persistence.UnitOfWork
{
    using Persistence.Repositories;

    class UnitOfWork : IUnitOfWork
    {
        readonly PhotoAlbumDbContext context;

        public IUsersRepository Users { get; }
        public ICategoriesRepository Categories { get; }
        public IImagesRepository Images { get; }

        internal UnitOfWork(PhotoAlbumDbContext context)
        {
            this.context = context;
            Users = new UsersRepository(context);
            Categories = new CategoriesRepository(context);
            Images = new ImagesRepository(context);
        }

        public void Complete() => context.SaveChanges();

        public void Dispose() => context.Dispose();
    }
}
