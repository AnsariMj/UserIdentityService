using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using UserIdentityService.Application.Common.Interfaces;

namespace UserIdentityService.Infrastructure.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _configuration;

    public TokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JwtSecurityToken GetToken(List<Claim> claimList)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var signingCredential = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            claims: claimList,
            signingCredentials: signingCredential
        );
        return token;
    }
}