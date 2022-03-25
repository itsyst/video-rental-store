using System.ComponentModel.DataAnnotations;
using Video.Domain.ValueObject;

namespace Video.Domain.Entities;

public class Customer
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Please enter customer's first name.")]
    [Display(Name = "First Name")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter customer's last name.")]
    [Display(Name = "Last Name")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Date of Birth")]
    [MinAge]
    public DateTime? Birthdate { get; set; }

    [Display(Name = "Is Subscribed")]
    public bool IsSubscribed { get; set; }

    public MembershipType? MembershipType { get; set; }

    [Display(Name = "Membership Type")]
    public byte MembershipTypeId { get; set; }
}
