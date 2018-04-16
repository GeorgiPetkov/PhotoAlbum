namespace Persistence.Repositories
{
    using Persistence.Entities;
    using static System.DateTime;

    class ImagesRepository : Repository<ImageEntity>, IImagesRepository
    {
        internal ImagesRepository(PhotoAlbumDbContext context) : base(context) { }

        public ImageEntity Add(string name, string description, byte[] data, CategoryEntity category)
        {
            return Set.Add(new ImageEntity
            {
                Name = name,
                CreatedOn = Now,
                Description = description,
                Data = data,
                Category = category
            });
        }

        public void Remove(ImageEntity image) => Set.Remove(image);
    }
}
