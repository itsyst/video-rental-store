using System.ComponentModel.DataAnnotations;

namespace Video.Application.Profiles.Dtos;

public class RentalDto
{
    [Key]
    public Guid CustomerId { get; set; }
    public List<int> MovieIds { get; set; } = new List<int>();
}
