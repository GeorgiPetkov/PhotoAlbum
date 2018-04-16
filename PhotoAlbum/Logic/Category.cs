namespace Logic
{
    using Persistence.Entities;
    using System;

    public class Category
    {
        public Guid Id { get; }
        public string Name { get; }

        internal Category(CategoryEntity category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    }
}
