namespace Logic
{
    using Persistence.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Persistence.UnitOfWork.UnitOfWorkProvider;

    class CategoriesManager : ICategoriesManager
    {
        public CategoryWithContent Get(Guid id) =>
            UsingUnitOfWork(unitOfWork => new CategoryWithContent(unitOfWork.Categories.GetWithContent(id)));

        public IEnumerable<Category> GetRootCategoriesByUser(Guid userId) =>
            UsingUnitOfWork(unitOfWork =>
                unitOfWork.Categories.GetRootCategoriesByUser(userId)
                    .Select(c => new Category(c)));

        public Category Create(string name, Guid userId, Guid? parentId)
        {
            return UsingUnitOfWork(unitOfWork =>
            {
                UserEntity user = unitOfWork.Users.Get(userId);
                CategoryEntity parent = (parentId == null) ? null : unitOfWork.Categories.Get(parentId.Value);
                CategoryEntity category = unitOfWork.Categories.Add(name, user, parent);
                unitOfWork.Complete();

                return new Category(category);
            });
        }

        public Category Rename(Guid id, string name)
        {
            return UsingUnitOfWork(unitOfWork =>
            {
                CategoryEntity category = unitOfWork.Categories.Get(id);
                category.Name = name;
                unitOfWork.Complete();

                return new Category(category);
            });
        }

        public void Delete(Guid id)
        {
            UsingUnitOfWork(unitOfWork =>
            {
                unitOfWork.Categories.Remove(unitOfWork.Categories.Get(id));
                unitOfWork.Complete();
            });
        }
    }
}
