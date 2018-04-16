namespace Persistence.UnitOfWork
{
    using System;

    public static class UnitOfWorkProvider
    {
        public static void UsingUnitOfWork(Action<IUnitOfWork> action)
        {
            UsingUnitOfWork((unitOfWork) => { action(unitOfWork); return (object)null; });
        }

        public static T UsingUnitOfWork<T>(Func<IUnitOfWork, T> action)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(new PhotoAlbumDbContext()))
            {
                return action(unitOfWork);
            }
        }
    }
}
