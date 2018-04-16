namespace Persistence.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Categories")]
    public class CategoryEntity : Entity
    {
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [Required]
        public UserEntity User { get; set; }
        public CategoryEntity Parent { get; set; }
        public ICollection<CategoryEntity> Categories { get; set; }
        public ICollection<ImageEntity> Images { get; set; }
    }
}
