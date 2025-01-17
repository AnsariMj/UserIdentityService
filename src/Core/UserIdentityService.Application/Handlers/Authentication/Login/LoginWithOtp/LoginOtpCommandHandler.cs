using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Application.Handlers.Authentication.Login.LoginWithOtp;


public class LoginOtpCommand : IRequest<LoginOtpCommandDto>
{
    public string Email { get; set; }
    public string OTP { get; set; }
}
public class LoginOtpCommandHandler : IRequestHandler<LoginOtpCommand, LoginOtpCommandDto>
{
    private readonly UserManager<ApplicatioinUser> _userManager;
    private readonly IMailKitEmailService _emailService;
    private readonly SignInManager<ApplicatioinUser> _signInManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IConfiguration _configuration;


    public LoginOtpCommandHandler(UserManager<ApplicatioinUser> userManager, IMailKitEmailService emailService, SignInManager<ApplicatioinUser> signInManager, ITokenGenerator TokenGenerator, IConfiguration configuration)
    {
        _userManager = userManager;
        _emailService = emailService;
        _signInManager = signInManager;
        _tokenGenerator = TokenGenerator;
        _configuration = configuration;
    }

    public async Task<LoginOtpCommandDto> Handle(LoginOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var signIn = await _signInManager.TwoFactorSignInAsync("Email", request.OTP, false, false);
        if (signIn.Succeeded)
        {
            if (user != null)
            {
                //Generating JWT Access Token
                var accessToken = await _tokenGenerator.GetAccessToken(user);

                //Generating Refresh Token
                user.RefreshToken = _tokenGenerator.GetRefreshToken();
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(int.Parse(_configuration["JWT:RefreshTokenExpiryTime"]));

                //Assigning RefreshToken and Expiry to user 
                await _userManager.UpdateAsync(user);

                return new LoginOtpCommandDto
                {
                    Message = $"{StatusCodes.Status200OK} User Login Successfully",
                    Status = "Success",
                    Token = accessToken,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiry = user.RefreshTokenExpiry,
                };
            }
        }
        return new LoginOtpCommandDto
        {
            Message = $"Something Were Wrong!",
            Status = "Error"
        };
    }
}
