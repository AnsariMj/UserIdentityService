using UserIdentityService.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using MediatR;

namespace UserIdentityService.Application.Handlers.Authentication.Login;

public class LoginCommand : IRequest<LoginCommandDto>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandDto>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IMailKitEmailService _emailService;

    public LoginCommandHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IMailKitEmailService emailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<LoginCommandDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //Checking the User Exist
        var user = await _userManager.FindByEmailAsync(request.Email);

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
            var jwtToken = GetToken(claimList);

            var token = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = jwtToken.ValidTo
            };

            return new LoginCommandDto
            {
                Message = "User Login Successfully",
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

    //Generating JWT Token
    private JwtSecurityToken GetToken(List<Claim> claimList)
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
