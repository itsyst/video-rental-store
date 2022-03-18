using System.ComponentModel.DataAnnotations;
using Video.Domain.ValueObject;

namespace Video.Application.Profiles.Dtos;

public class CustomerDto
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Please enter customer's first name.")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter customer's last name.")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MinAge]
    public DateTime? Birthdate { get; set; }

    public bool IsSubscribed { get; set; }
    public MembershipTypeDto? MembershipTypeDto { get; set; }
    public byte MembershipTypeId { get; set; }
}
