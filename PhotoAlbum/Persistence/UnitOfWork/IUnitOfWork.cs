namespace Persistence.UnitOfWork
{
    using Persistence.Repositories;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
        ICategoriesRepository Categories { get; }
        IImagesRepository Images { get; }

        void Complete();
    }
}
