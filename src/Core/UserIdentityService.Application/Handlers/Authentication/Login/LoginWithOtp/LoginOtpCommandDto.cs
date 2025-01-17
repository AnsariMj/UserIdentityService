namespace UserIdentityService.Application.Handlers.Authentication.Login.LoginWithOtp;

public class LoginOtpCommandDto
{
    public string? Message { get; set; }
    public string? Status { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }

}
