namespace Logic
{
    using Persistence.Entities;
    using System;

    public class Image
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime CreatedOn { get; }
        public string Description { get; }
        public byte[] Data { get; }

        internal Image(ImageEntity image)
        {
            Id = image.Id;
            Name = image.Name;
            CreatedOn = image.CreatedOn;
            Description = image.Description;
            Data = image.Data;
        }
    }
}
