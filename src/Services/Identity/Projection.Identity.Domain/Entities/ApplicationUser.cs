using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Projection.Identity.Domain.Entities;

public class ApplicationUser(DateTime createdDate) : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedDate { get; private set; } = createdDate;

    // Constructor
    public ApplicationUser() : this(DateTime.UtcNow)
    {
    }
}
