using Microsoft.AspNetCore.Identity;

namespace UserIdentityService.Domain.Models;

public class ApplicatioinUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}
