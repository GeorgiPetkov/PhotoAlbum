namespace Persistence.Repositories
{
    using Extensions;
    using Persistence.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    class CategoriesRepository : Repository<CategoryEntity>, ICategoriesRepository
    {
        internal CategoriesRepository(PhotoAlbumDbContext context) : base(context) { }

        public CategoryEntity GetWithContent(Guid id)
        {
            return Set
                .Include(c => c.Categories)
                .Include(c => c.Images)
                .Single(c => c.Id == id);
        }

        public IEnumerable<CategoryEntity> GetRootCategoriesByUser(Guid userId) =>
            Set.Where(c => c.User.Id == userId && c.Parent == null).ToList();

        public CategoryEntity Add(string name, UserEntity user, CategoryEntity parent)
        {
            return Set.Add(new CategoryEntity
            {
                Name = name,
                User = user,
                Parent = parent
            });
        }

        /* The default implementation with cascade delete might have been enough
         * if it wasn't for SQL Server's SAME TABLE REFERENCE constraint. */
        public void Remove(CategoryEntity category) => RemoveAll(category.Id.ToEnumerable());

        void RemoveAll(IEnumerable<Guid> categoriesIds)
        {
            if (categoriesIds.IsEmpty())
            {
                return;
            }

            IEnumerable<CategoryEntity> categories = Set
                .Include(c => c.Categories)
                .Where(c => categoriesIds.Contains(c.Id))
                .ToList();

            RemoveAll(categories.SelectMany(c => c.Categories).Select(c => c.Id));
            Set.RemoveRange(categories);
        }
    }
}
