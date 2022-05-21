using System.ComponentModel.DataAnnotations;

namespace Video.Application.Profiles.Dtos;

public class RentalDto
{
    [Key]
    public int Id { get; set; }
    public CustomerDto? Customer { get; set; }
    public MovieDto Movie { get; set; }
    public DateTime DateReturned { get; set; }
    public DateTime DateRented { get; set; }
}
