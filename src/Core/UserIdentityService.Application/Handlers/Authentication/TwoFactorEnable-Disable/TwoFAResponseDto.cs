namespace UserIdentityService.Application.Handlers.Authentication.TwoFactorEnable_Disable;

public class TwoFAResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public bool? TwoFactorEnabled { get; set; }
}