using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Video.Domain;

public class Movie
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Poster")]
    public string? ImageUrl { get; set; }

    [Required]
    [Display(Name ="Release Date")]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [Display(Name ="Created Date")]
    public DateTime CreatedDate { get; set; }

    [Required]
    [Range(1, 10)]
    public int InStoch { get; set; }

    public Genre Genre { get; set; } = new();

    [Required]
    [Display(Name = "Genre")]
    [ForeignKey("GenreId")]
    public Guid GenreId { get; set; }
}
