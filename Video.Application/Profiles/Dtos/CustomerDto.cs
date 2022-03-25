using System.ComponentModel.DataAnnotations;

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
    [MinAgeDto]
    public DateTime? Birthdate { get; set; }
    public bool IsSubscribed { get; set; }
    public MembershipTypeDto? MembershipType { get; set; }
    public byte MembershipTypeId { get; set; }
}
