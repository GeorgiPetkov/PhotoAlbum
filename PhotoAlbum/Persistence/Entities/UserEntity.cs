namespace Persistence.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Users")]
    public class UserEntity : Entity
    {
        [Required, Index(IsUnique = true), MaxLength(32)]
        public string Username { get; set; }
        [Required, Column(TypeName = "BINARY"), MaxLength(32)]
        public byte[] PasswordHash { get; set; }
        [Required, MaxLength(64)]
        public string FullName { get; set; }
        [Required, MaxLength(64)]
        public string Email { get; set; }
        public ICollection<CategoryEntity> Categories { get; set; }
    }
}
