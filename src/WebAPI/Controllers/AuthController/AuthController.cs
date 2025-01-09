using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentityService.API.Controllers.BaseController;
using UserIdentityService.Application.Handlers.Authentication.Login.LoginWithOtp;
using UserIdentityService.Application.Handlers.Authentication.Login.LoginWithoutOtp;
using UserIdentityService.Application.Handlers.Authentication.Register;
using UserIdentityService.Application.Handlers.ConfirmationEmail;

namespace UserIdentityService.API.Controllers.AuthController;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApiController
{
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(404)]
    [HttpPost("Register")]
    public async Task<ActionResult<RegisterCommand>> Register([FromQuery] RegisterCommand command)
    {
        var response = await Mediator.Send(command);
        return Ok(response);
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(404)]
    [HttpPost("login")]
    public async Task<ActionResult<LoginCommand>> Login([FromQuery] LoginCommand loginCommand, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(loginCommand);
        return Ok(response);
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("login-2FA")]
    public async Task<ActionResult<LoginOtpCommand>> LoginWithOtp([FromQuery] LoginOtpCommand LoginOtpCommand, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(LoginOtpCommand);
        return Ok(response);
    }

    [HttpGet("ConfirmEmail")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<ActionResult<ConfirmEmailCommand>> ConfirmEmail([FromQuery] ConfirmEmailCommand confirmEmailCommand, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(confirmEmailCommand);
        return Ok(response);
    }
}
