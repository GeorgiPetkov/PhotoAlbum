namespace Logic
{
    using System;

    public interface IImagesManager
    {
        Image Create(string name, string description, byte[] data, Guid categoryId);

        Image Edit(Guid id, string name, string description);

        void Delete(Guid id);
    }
}
