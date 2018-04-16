namespace Persistence.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Images")]
    public class ImageEntity : Entity
    {
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        [Required]
        public byte[] Data { get; set; }
        [Required]
        public CategoryEntity Category { get; set; }
    }
}
