using UserIdentityService.Application.Common;

namespace UserIdentityService.Application.Handlers.Authentication.Register;

public class RegiserCommandDto
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<string>? Role { get; set; }
    public Response? Response { get; set; }
}
