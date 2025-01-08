using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UserIdentityService.Application.Common;
using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Application.Common.Services;

namespace UserIdentityService.Application.Handlers.Authentication.Register;

public class RegisterCommand : IRequest<RegiserCommandDto>
{
    public string? Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? role { get; set; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegiserCommandDto>
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IMailKitEmailService _emailService;


    public RegisterCommandHandler(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration,
        IMailKitEmailService emailService
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<RegiserCommandDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Check User Exists
        var userExists = await _userManager.FindByEmailAsync(request.Email);
        if (userExists != null)
        {
            return new RegiserCommandDto
            {
                Response = new Response
                {
                    Status = "Error",
                    Message = "User already exists",
                    IsSuccess = false
                }
            };
        }

        //Add the user in Db
        IdentityUser user = new()
        {
            UserName = request.Username,
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        //checl the role exists or not if not add 
        if (await _roleManager.RoleExistsAsync(request.role))
        {
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new RegiserCommandDto
                {
                    Response = new Response
                    {
                        Status = "Error",
                        Message = "Fail to create user",
                        IsSuccess = false
                    }
                };
            }

            //Add Role to the user
            await _userManager.AddToRoleAsync(user, request.role);

            //Generating confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // getting base Url 
            var appBaseUrl = _configuration["AppBaseUrl"];

            //sending token and email to "/api/Auth/ConfirmEmail" route for Email confirmation 
            var encodedToken = Uri.EscapeDataString(token);
            var encodedEmail = Uri.EscapeDataString(user.Email);
            var confirmationLink = $"{appBaseUrl}/api/Auth/ConfirmEmail?token={encodedToken}&email={encodedEmail}";

            //Sending confirmation email to specific user
            var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink);
            _emailService.SendEmail(message);

            return new RegiserCommandDto
            {
                UserName = request.Username,
                Email = request.Email,
                Role = request.role,
                Response = new Response
                {
                    Status = "Success",
                    Message = $"User Created and Email verification link send to {request.Email} Successfully",
                    IsSuccess = true
                }
            };
        }
        else
        {
            return new RegiserCommandDto
            {
                Response = new Response
                {
                    Status = "Error",
                    Message = "This Role Does not exists",
                    IsSuccess = false
                }
            };
        }
    }
    //Add Token to Verify the email..
}
