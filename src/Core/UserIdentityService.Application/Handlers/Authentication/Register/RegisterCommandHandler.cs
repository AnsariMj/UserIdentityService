using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UserIdentityService.Application.Common;
using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Application.Common.Services;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Application.Handlers.Authentication.Register;

public class RegisterCommand : IRequest<RegiserCommandDto>
{
    public string? Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IEnumerable<string>? role { get; set; } = new List<string>();
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegiserCommandDto>
{

    private readonly UserManager<ApplicatioinUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicatioinUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IMailKitEmailService _emailService;


    public RegisterCommandHandler(
        UserManager<ApplicatioinUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicatioinUser> signInManager,
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
        // Check if the user already exists
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

        // Add the user to the database
        ApplicatioinUser user = new()
        {
            UserName = request.Username,
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

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

        // Assign roles to the user
        if (request.role != null && request.role.Any())
        {
            var roleError = new List<string>();

            foreach (var role in request.role)
            {
                // Check if the role exists
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    roleError.Add(role);
                    continue;
                }

                // Assign the role
                var addToRolesResult = await _userManager.AddToRoleAsync(user, role);
                if (!addToRolesResult.Succeeded)
                {
                    return new RegiserCommandDto
                    {
                        Response = new Response
                        {
                            Status = "Error",
                            Message = $"Failed to assign role '{role}' to the user",
                            IsSuccess = false,
                        }
                    };
                }
            }

            // Handle missing roles
            if (roleError.Any())
            {
                return new RegiserCommandDto
                {
                    Response = new Response
                    {
                        Status = "Error",
                        Message = $"The following roles do not exist: {string.Join(", ", roleError)}",
                        IsSuccess = false
                    }
                };
            }
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        // getting base Url 
        var appBaseUrl = _configuration["AppBaseUrl"];

        //sending token and email to "/api/Auth/ConfirmEmail" route for Email confirmation 
        var encodedToken = Uri.EscapeDataString(token);
        var encodedEmail = Uri.EscapeDataString(user.Email);
        var confirmationLink = $"{appBaseUrl}/api/Auth/ConfirmEmail?token={encodedToken}&email={encodedEmail}";

        // Send confirmation email
        var message = new Message(new string[] { user.Email }, "Confirmation Email Link", confirmationLink);
        _emailService.SendEmail(message);

        return new RegiserCommandDto
        {
            UserName = request.Username,
            Email = request.Email,
            Role = (List<string>)request.role,
            Response = new Response
            {
                Status = "Success",
                Message = $"User created and email verification link sent to {request.Email} successfully.",
                IsSuccess = true
            }
        };
    }
}
