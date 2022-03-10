using System.ComponentModel.DataAnnotations;

namespace Video.Domain;

public class Customer
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Display(Name="First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;
}
