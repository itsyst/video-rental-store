using System.ComponentModel.DataAnnotations;

namespace Video.Domain.Entities;

public class Movie
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Poster")]
    public string? ImageUrl { get; set; }

    [Required]
    [Display(Name = "Release Date")]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; }

    [Required]
    [Range(1, 10)]
    [Display(Name = "In Stock")]
    public int InStock { get; set; }

    [Required]
    [Display(Name = "Genre")]
    public ushort GenreId { get; set; }
    public Genre? Genre { get; set; }
    public string? Overview { get; set; }

}
