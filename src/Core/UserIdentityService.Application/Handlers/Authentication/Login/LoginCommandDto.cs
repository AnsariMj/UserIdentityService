namespace UserIdentityService.Application.Handlers.Authentication.Login;

public class LoginCommandDto
{
    public string? Message { get; set; }
    public string? Status { get; set; }
    public string? Token { get; set; }
}
