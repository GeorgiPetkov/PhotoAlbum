namespace Persistence.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using static System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

    public abstract class Entity
    {
        [DatabaseGenerated(Identity)]
        public Guid Id { get; private set; }
    }
}
