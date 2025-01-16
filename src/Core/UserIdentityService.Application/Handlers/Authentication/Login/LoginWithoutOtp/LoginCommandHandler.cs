using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Application.Common.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using MediatR;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Application.Handlers.Authentication.Login.LoginWithoutOtp;

public class LoginCommand : IRequest<LoginCommandDto>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandDto>
{
    private readonly UserManager<ApplicatioinUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicatioinUser> _signInManager;
    private readonly IMailKitEmailService _emailService;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginCommandHandler(
        UserManager<ApplicatioinUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicatioinUser> signInManager,
        IMailKitEmailService emailService,
        ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginCommandDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //Checking the User Exist
        var user = await _userManager.FindByEmailAsync(request.Email);

        //If Two factored enabled
        if (user.TwoFactorEnabled)
        {
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(user, request.Password, false, true);
            var otp = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            var message = new Message(new string[] { user.Email }, "Confirmation email link", otp);
            _emailService.SendEmail(message);

            return new LoginCommandDto
            {
                Message = $"We have send OTP to {request.Email}, Please Verify!",
                Status = "Success",
            };

        }

        //Checking valid Password
        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            //creating claim list 
            var claimList = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            // add roles to the list
            foreach (var role in userRoles)
            {
                claimList.Add(new Claim(ClaimTypes.Role, role));
            }

            //generate token with claims
            var jwtToken = _tokenGenerator.GetToken(claimList);

            var token = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = jwtToken.ValidTo
            };

            return new LoginCommandDto
            {
                Message = $"{StatusCodes.Status200OK} User Login Successfully",
                Status = "Success",
                Token = $"{token}"
            };
        }

        return new LoginCommandDto
        {
            Message = "User Does't exists",
            Status = "Error"
        };
    }
}
