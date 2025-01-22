namespace UserIdentityService.Application.Handlers.Authentication.Login.ExternalLogin;
public class GoogleLoginCommandDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}

