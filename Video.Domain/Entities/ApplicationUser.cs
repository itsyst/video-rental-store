using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Video.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(55)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(55)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    [Required]
    public string? State { get; set; }
    public string? PostalCode { get; set; }
}
