using UserIdentityService.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using MediatR;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using UserIdentityService.Application.Handlers.Authentication.Login.LoginWithoutOtp;
using Microsoft.AspNetCore.Http;
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
    private readonly ITokenGenerator _TokenGenerator;


    public LoginOtpCommandHandler(UserManager<ApplicatioinUser> userManager, IMailKitEmailService emailService, SignInManager<ApplicatioinUser> signInManager, ITokenGenerator TokenGenerator)
    {
        _userManager = userManager;
        _emailService = emailService;
        _signInManager = signInManager;
        _TokenGenerator = TokenGenerator;
    }

    public async Task<LoginOtpCommandDto> Handle(LoginOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var signIn = await _signInManager.TwoFactorSignInAsync("Email", request.OTP, false, false);
        if (signIn.Succeeded)
        {
            if (user != null)
            {
                var claimList = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, request.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    claimList.Add(new Claim(ClaimTypes.Role, role));
                }
                var jwtToken = _TokenGenerator.GetToken(claimList);
                var token = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                };
                return new LoginOtpCommandDto
                {
                    Message = "User Login Successfully",
                    Status = "Success",
                    Token = $"{token}"
                };
            }

        }
        return new LoginOtpCommandDto
        {
            Message = $"Something Were Wrong!",
            Status = "Errro"
        };
    }
}
