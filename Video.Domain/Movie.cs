using System.ComponentModel.DataAnnotations;

namespace Video.Domain;

public class Movie
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Poster")]
    public string? ImageUrl { get; set; }
}
