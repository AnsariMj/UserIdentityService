using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Application.Common.Services;
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
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(
        UserManager<ApplicatioinUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicatioinUser> signInManager,
        IMailKitEmailService emailService,
        ITokenGenerator tokenGenerator,
        IConfiguration configuration
      )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _tokenGenerator = tokenGenerator;
        _configuration = configuration;
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
            //Generating JWT Access Token
            var accessToken = await _tokenGenerator.GetAccessToken(user);

            //Generating Refresh Token
            user.RefreshToken = _tokenGenerator.GetRefreshToken();
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(int.Parse(_configuration["JWT:RefreshTokenExpiryTime"]));

            //Assigning RefreshToken and Expiry to user 
            await _userManager.UpdateAsync(user);
            return new LoginCommandDto
            {
                Message = $"{StatusCodes.Status200OK} User Login Successfully",
                Status = "Success",
                Token = accessToken,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiry = user.RefreshTokenExpiry,
            };
        }

        return new LoginCommandDto
        {
            Message = "User Does't exists",
            Status = "Error"
        };
    }
}
