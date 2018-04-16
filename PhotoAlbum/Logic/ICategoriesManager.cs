namespace Logic
{
    using System;
    using System.Collections.Generic;

    public interface ICategoriesManager
    {
        CategoryWithContent Get(Guid id);

        IEnumerable<Category> GetRootCategoriesByUser(Guid userId);

        Category Create(string name, Guid userId, Guid? parentId);

        Category Rename(Guid id, string name);

        void Delete(Guid id);
    }
}
