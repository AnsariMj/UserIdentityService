using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserIdentityService.Domain.Models;

namespace UserIdentityService.Application.Common.Interfaces;

public interface ITokenGenerator
{
    public Task<string> GetAccessToken(ApplicatioinUser user);

    public string GetRefreshToken();
}
