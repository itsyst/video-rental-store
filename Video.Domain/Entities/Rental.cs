using System.ComponentModel.DataAnnotations;

namespace Video.Domain.Entities;

public class Rental
{
    [Key]
    public int Id { get; set; }
    [Required]
    public Customer Customer { get; set; }

    [Required]
    public Movie Movie { get; set; }

    public DateTime DateReturned { get; set; }
    public DateTime DateRented { get; set; }
}
