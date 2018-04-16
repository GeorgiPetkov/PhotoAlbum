namespace Logic
{
    using Persistence.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryWithContent : Category
    {
        public IEnumerable<Category> Categories { get; }
        public IEnumerable<Image> Images { get; }

        internal CategoryWithContent(CategoryEntity category) : base(category)
        {
            Categories = category.Categories.Select(c => new Category(c));
            Images = category.Images.Select(i => new Image(i));
        }
    }
}
