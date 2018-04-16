namespace Persistence.Repositories
{
    using Persistence.Entities;

    public interface IImagesRepository : IRepository<ImageEntity>
    {
        ImageEntity Add(string name, string description, byte[] data, CategoryEntity category);

        void Remove(ImageEntity image);
    }
}
