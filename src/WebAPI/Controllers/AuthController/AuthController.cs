using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentityService.API.Controllers.BaseController;
using UserIdentityService.Application.Handlers.Authentication.Register;
using UserIdentityService.Application.Handlers.ConfirmationEmail;

namespace UserIdentityService.API.Controllers.AuthController;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApiController
{
    [AllowAnonymous]
    [HttpPost("Register")]
    //[Route("Register")]
    public async Task<ActionResult<RegisterCommand>> Register([FromBody] RegisterCommand command)
    {
        var response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("ConfirmEmail")]
    public async Task<ActionResult<ConfirmEmailCommand>> ConfirmEmail([FromQuery] ConfirmEmailCommand confirmEmailCommand, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(confirmEmailCommand);
        return Ok(response);
    }

}
