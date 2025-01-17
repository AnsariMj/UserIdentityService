namespace UserIdentityService.Application.Handlers.Authentication.RefreshToken;

public class RefreshTokenCommandDto
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
    public string Message { get; set; }
}
