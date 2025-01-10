using System.ComponentModel.DataAnnotations;

namespace UserIdentityService.Application.Handlers.Authentication.ChangePassword.ForgetPassword;

public class ForgetPasswordDto
{
    public bool? Success { get; set; }
    public string? Message { get; set; }
    public string? Token { get; set; }
}
