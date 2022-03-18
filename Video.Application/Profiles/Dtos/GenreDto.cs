using System.ComponentModel.DataAnnotations;

namespace Video.Application.Profiles.Dtos;

public class GenreDto
{
    [Key]
    public ushort Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;
}
