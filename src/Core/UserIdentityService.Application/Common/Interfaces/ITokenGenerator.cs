using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UserIdentityService.Application.Common.Interfaces;

public interface ITokenGenerator
{
   public JwtSecurityToken GetToken(List<Claim> claimList);
}
