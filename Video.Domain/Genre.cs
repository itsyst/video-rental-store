using System.ComponentModel.DataAnnotations;

namespace Video.Domain
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;
    }
}