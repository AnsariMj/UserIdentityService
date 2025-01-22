using System.Reflection.Metadata;
using System.Security.Claims;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Configuration;
using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Application.Handlers.Authentication.Login.ExternalLogin;

public class GoogleLoginCommand : IRequest<GoogleLoginCommandDto>
{
    public string? idToken { get; set; }
}

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, GoogleLoginCommandDto>
{
    private readonly SignInManager<ApplicatioinUser> _signInManager;
    private readonly UserManager<ApplicatioinUser> _userManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IConfiguration _configuration;


    public GoogleLoginCommandHandler(SignInManager<ApplicatioinUser> signInManager, UserManager<ApplicatioinUser> userManager, ITokenGenerator tokenGenerator, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
        _configuration = configuration;
    }

    public async Task<GoogleLoginCommandDto> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        var idToken = request.idToken;
        if (idToken is null )
        {
            return new GoogleLoginCommandDto
            {
                IsSuccess = false,
                Message = "Invalid Id Token."
            };
        }
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { _configuration["Google:ClientId"] }
        };

        // Validate the Google token
        var response = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        // Check if the user exists
        var user = await _userManager.FindByEmailAsync(response.Email);
        if (user == null)
        {
            user = new ApplicatioinUser { UserName = response.Name, Email = response.Email };
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return new GoogleLoginCommandDto
                {
                    IsSuccess = false,
                    Message = "Failed to create user."
                };
            }
            var loginInfo = new UserLoginInfo("Google", response.Subject, response.Email);
            await _userManager.AddLoginAsync(user, loginInfo);
        }
        await _signInManager.SignInAsync(user, isPersistent: false);

        var accessToken = await _tokenGenerator.GetAccessToken(user);
        user.RefreshToken = _tokenGenerator.GetRefreshToken();
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(int.Parse(_configuration["JWT:RefreshTokenExpiryTime"]));

        await _userManager.UpdateAsync(user);

        return new GoogleLoginCommandDto
        {
            Message = $"User Login Successfully",
            IsSuccess = true,
            Token = accessToken,
            RefreshToken = user.RefreshToken,
            RefreshTokenExpiry = user.RefreshTokenExpiry,
        };
    }
}