using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Application.Common.Services;
using UserIdentityService.Application.Handlers.Authentication.Login.LoginWithoutOtp;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Application.Handlers.Authentication.RefreshToken;


public class RefreshTokenCommand : IRequest<RefreshTokenCommandDto>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandDto>
{
    private readonly UserManager<ApplicatioinUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicatioinUser> _signInManager;
    private readonly IMailKitEmailService _emailService;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IConfiguration _configuration;

    public RefreshTokenCommandHandler(
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

    public async Task<RefreshTokenCommandDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principle = GetClaimsPrincipal(request.AccessToken);
        if (principle?.Identity?.Name == null)
        {
            return new RefreshTokenCommandDto
            {
                Message = "Invalid access token"
            };
        }

        var user = await _userManager.FindByNameAsync(principle.Identity.Name);
        if (user == null || user.RefreshToken != request.RefreshToken)
        {
            return new RefreshTokenCommandDto
            {
                Message = "Invalid or expired refresh token"
            };
        }

        var token = await _tokenGenerator.GetAccessToken(user);
        var tokenExpiration = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:TokenExpiryTime"]));

        return new RefreshTokenCommandDto
        {
            AccessToken = token,
            Expiration = tokenExpiration,
            Message = "Token refreshed successfully"
        };
    }

    //Getting Details of current user 
    public ClaimsPrincipal GetClaimsPrincipal(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

        return principle;
    }
}