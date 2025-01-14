using MediatR;
using Microsoft.AspNetCore.Identity;

namespace UserIdentityService.Application.Handlers.Authentication.TwoFactorEnable_Disable;

public class Get2FAQuery : IRequest<TwoFAResponseDto>
{
    public string Email { get; set; }
}

public class Get2FAQueryHandler : IRequestHandler<Get2FAQuery, TwoFAResponseDto>
{
    private readonly UserManager<IdentityUser> _userManager;

    public Get2FAQueryHandler(UserManager<IdentityUser> userManager)
     {
        _userManager = userManager;
    }

    public async Task<TwoFAResponseDto> Handle(Get2FAQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        { 
            return new TwoFAResponseDto
            {
                Success = false,
                Message = "User not found."
            };
        }
        return new TwoFAResponseDto
        {
            Success = true,
            Message = user.TwoFactorEnabled
                   ? "Two-Factor Authentication is enabled."
                   : "Two-Factor Authentication is disabled.",
            TwoFactorEnabled = user.TwoFactorEnabled
        };
    }
}
