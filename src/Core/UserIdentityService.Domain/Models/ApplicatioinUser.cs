using Microsoft.AspNetCore.Identity;

namespace UserIdentityService.Domain.Models;

public class ApplicatioinUser : IdentityUser
{
    public string? RefreshToekn { get; set; }
    public DateTime? RefreshToeknExpiry { get; set; }
}
