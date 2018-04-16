namespace Persistence
{
    using Persistence.Entities;
    using System;
    using System.Data.Entity;

    class PhotoAlbumDbContext : DbContext
    {
        public PhotoAlbumDbContext() : base("PhotoAlbum")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.Log = Console.Write;
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
    }
}
