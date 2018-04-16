namespace Persistence.Repositories
{
    using Persistence.Entities;
    using System;
    using System.Collections.Generic;

    public interface ICategoriesRepository : IRepository<CategoryEntity>
    {
        CategoryEntity GetWithContent(Guid id);

        IEnumerable<CategoryEntity> GetRootCategoriesByUser(Guid userId);

        CategoryEntity Add(string name, UserEntity user, CategoryEntity parent);

        void Remove(CategoryEntity category);
    }
}
