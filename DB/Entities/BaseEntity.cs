using System.ComponentModel.DataAnnotations;

namespace DB.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
