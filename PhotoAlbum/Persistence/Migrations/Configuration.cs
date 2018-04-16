namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.SqlServer;

    sealed class Configuration : DbMigrationsConfiguration<PhotoAlbumDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ForceAddingOfProviderAsDependency();
        }

        static Type ForceAddingOfProviderAsDependency() => typeof(SqlProviderServices);
    }
}
