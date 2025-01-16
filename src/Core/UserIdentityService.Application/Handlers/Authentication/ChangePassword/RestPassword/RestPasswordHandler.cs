using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Application.Handlers.Authentication.ChangePassword.RestPassword;

public class RestPassword : IRequest<ResetPasswordDto>
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
}
public class RestPasswordHandler : IRequestHandler<RestPassword, ResetPasswordDto>
{
    private readonly UserManager<ApplicatioinUser> _userManager;

    public RestPasswordHandler(UserManager<ApplicatioinUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ResetPasswordDto> Handle(RestPassword request, CancellationToken cancellationToken)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new ResetPasswordDto
            {
                Success = false,
                Message = "Invalid email or token."
            };
        }

        // Reset password
        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
        if (!result.Succeeded)
        {
            return new ResetPasswordDto
            {
                Success = false,
                Message = string.Join(", ", result.Errors.Select(e => e.Description))
            };
        }

        return new ResetPasswordDto
        {
            Success = true,
            Message = "Password has been Change successfully."
        };
    }
}
