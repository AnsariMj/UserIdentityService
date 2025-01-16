using MediatR;
using Microsoft.AspNetCore.Identity;
using UserIdentityService.Application.Common;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Application.Handlers.Authentication.ConfirmationEmail;

public class ConfirmEmailCommand : IRequest<Response>
{
    public string token { get; set; }
    public string email { get; set; }
}

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Response>
{
    private readonly UserManager<ApplicatioinUser> _userManager;

    public ConfirmEmailCommandHandler(UserManager<ApplicatioinUser> userManager)
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
