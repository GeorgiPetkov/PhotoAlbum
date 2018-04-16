namespace Logic
{
    using Persistence.Entities;
    using System;
    using static Persistence.UnitOfWork.UnitOfWorkProvider;

    class ImagesManager : IImagesManager
    {
        public Image Create(string name, string description, byte[] data, Guid categoryId)
        {
            return UsingUnitOfWork(unitOfWork =>
            {
                CategoryEntity category = unitOfWork.Categories.Get(categoryId);
                ImageEntity image = unitOfWork.Images.Add(name, description, data, category);
                unitOfWork.Complete();

                return new Image(image);
            });
        }

        public Image Edit(Guid id, string name, string description)
        {
            return UsingUnitOfWork(unitOfWork =>
            {
                ImageEntity image = unitOfWork.Images.Get(id);
                image.Name = name;
                image.Description = description;
                unitOfWork.Complete();

                return new Image(image);
            });
        }

        public void Delete(Guid id)
        {
            UsingUnitOfWork(unitOfWork =>
            {
                unitOfWork.Images.Remove(unitOfWork.Images.Get(id));
                unitOfWork.Complete();
            });
        }
    }
}
