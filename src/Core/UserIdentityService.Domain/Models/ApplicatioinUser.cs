using Microsoft.AspNetCore.Identity;
using UserIdentityService.Domain.Entities;

namespace UserIdentityService.Domain.Models;

public class ApplicatioinUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
