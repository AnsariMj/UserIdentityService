using MediatR;
using Microsoft.AspNetCore.Identity;
using UserIdentityService.Application.Common;

namespace UserIdentityService.Application.Handlers.Authentication.ConfirmationEmail;

public class ConfirmEmailCommand : IRequest<Response>
{
    public string token { get; set; }
    public string email { get; set; }
}

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Response>
{
    private readonly UserManager<IdentityUser> _userManager;

    public ConfirmEmailCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Response> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.email);
        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, request.token);
            if (result.Succeeded)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Email Verified Succesfully",
                    Status = "Success"
                };
            }
        }

        return new Response
        {
            IsSuccess = false,
            Message = "User Does't exists!",
            Status = "Error"
        };
    }
}
