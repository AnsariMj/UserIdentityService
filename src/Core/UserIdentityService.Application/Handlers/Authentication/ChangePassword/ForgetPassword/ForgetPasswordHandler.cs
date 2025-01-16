using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Application.Common.Services;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Application.Handlers.Authentication.ChangePassword.ForgetPassword;


public class ForgetPassword : IRequest<ForgetPasswordDto>
{
    public string Email { get; set; }
}
public class ForgetPasswordHandler : IRequestHandler<ForgetPassword, ForgetPasswordDto>
{
    private readonly UserManager<ApplicatioinUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMailKitEmailService _emailService;

    public ForgetPasswordHandler(UserManager<ApplicatioinUser> userManager, IConfiguration configuration, IMailKitEmailService emailService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<ForgetPasswordDto> Handle(ForgetPassword request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user != null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var appBaseUrl = _configuration["AppBaseUrl"];
            var encodedToken = Uri.EscapeDataString(token);
            var passwordResetLink = $"{appBaseUrl}/api/Auth/reset-password?token={encodedToken}&email={Uri.EscapeDataString(user.Email)}";

            var message = new Message(
                new string[] { user.Email },
                "Password Reset Request",
                 $"Please use the following link to reset your password: {passwordResetLink}"
            );

            _emailService.SendEmail(message);
            return new ForgetPasswordDto
            {
                Success = true,
                Message = "Password reset link has been sent to the provided email address.",
                Token = token
            };

        }
        return new ForgetPasswordDto
        {
            Success = false,
            Message = "Something went wrong!"
        };
    }
}
