using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using UserIdentityService.Application.Common.Interfaces;
using System.Security.Cryptography;
using UserIdentityService.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace UserIdentityService.Infrastructure.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicatioinUser> _userManager;


    public TokenGenerator(IConfiguration configuration, UserManager<ApplicatioinUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    //Generating JWT Access Token
    public async Task<string> GetAccessToken(ApplicatioinUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var signingCredential = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Email,user.Email)
        };

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        _ = int.TryParse(_configuration["JWT:TokenExpiryTime"], out var TokenExpiryTime);
        var expirationTimeUTC = DateTime.UtcNow.AddMinutes(TokenExpiryTime);

        var token = new JwtSecurityToken
        (
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: expirationTimeUTC,
            claims: claims,
            signingCredentials: signingCredential
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //Generating Refresh Token
    public string GetRefreshToken()
    {
        var randomNumber = new Byte[32];
        var range = RandomNumberGenerator.Create();
        range.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}