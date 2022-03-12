using System.ComponentModel.DataAnnotations;

namespace Video.Domain;

public class Customer
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Display(Name="First Name")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Date of Birth")]
    public DateTime? Birthdate { get; set; }

    [Display(Name = "Is Subscribed")]
    public bool IsSubscribed { get; set; }

    public MembershipType? MembershipType { get; set; }

    [Display(Name = "Membership Type")]
    public byte MembershipTypeId { get; set; }
}
