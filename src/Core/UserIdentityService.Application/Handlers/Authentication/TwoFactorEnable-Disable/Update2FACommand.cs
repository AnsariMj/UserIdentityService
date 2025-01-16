using MediatR;
using Microsoft.AspNetCore.Identity;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Application.Handlers.Authentication.TwoFactorEnable_Disable;
public class Update2FACommand : IRequest<TwoFAResponseDto>
{
    public string Email { get; set; }
    public bool Enable2FA { get; set; }
}
public class Update2FACommandHandler : IRequestHandler<Update2FACommand, TwoFAResponseDto>
{
    private readonly UserManager<ApplicatioinUser> _userManager;

    public Update2FACommandHandler(UserManager<ApplicatioinUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<TwoFAResponseDto> Handle(Update2FACommand request, CancellationToken cancellationToken)
    {
        // Find the user
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new TwoFAResponseDto
            {
                Success = false,
                Message = "User not found."
            };
        }

        // Update 2FA setting
        var result = await _userManager.SetTwoFactorEnabledAsync(user, request.Enable2FA);
        if (!result.Succeeded)
        {
            return new TwoFAResponseDto
            {
                Success = false,
                Message = "Failed to update Two-Factor Authentication setting."
            };
        }

        return new TwoFAResponseDto
        {
            Success = true,
            Message = request.Enable2FA
               ? "Two-Factor Authentication is enabled."
               : "Two-Factor Authentication is disabled.",
            TwoFactorEnabled = request.Enable2FA
        };
    }
}