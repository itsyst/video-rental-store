using System.ComponentModel.DataAnnotations;

namespace Video.Application.Profiles.Dtos;

public class MovieDto
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [Required]
    public string? ImageUrl { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    [Range(1, 10)]
    public int InStock { get; set; }

    [Required]
    public ushort GenreId { get; set; }

    public GenreDto? Genre { get; set; }
    public string? Overview { get; set; }

}
