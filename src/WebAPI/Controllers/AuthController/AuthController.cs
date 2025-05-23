﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using UserIdentityService.API.Controllers.BaseController;
using UserIdentityService.Application.Common;
using UserIdentityService.Application.Handlers.Authentication.ChangePassword.ForgetPassword;
using UserIdentityService.Application.Handlers.Authentication.ChangePassword.RestPassword;
using UserIdentityService.Application.Handlers.Authentication.ConfirmationEmail;
using UserIdentityService.Application.Handlers.Authentication.Login.ExternalLogin;
using UserIdentityService.Application.Handlers.Authentication.Login.LoginWithOtp;
using UserIdentityService.Application.Handlers.Authentication.Login.LoginWithoutOtp;
using UserIdentityService.Application.Handlers.Authentication.RefreshToken;
using UserIdentityService.Application.Handlers.Authentication.Register;
using UserIdentityService.Application.Handlers.Authentication.TwoFactorEnable_Disable;

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
    public async Task<ActionResult<RegiserCommandDto>> Register([FromQuery] RegisterCommand command)
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
    public async Task<ActionResult<LoginCommandDto>> Login([FromBody] LoginCommand loginCommand, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(loginCommand);
        return Ok(response);
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("login-2FA")]
    public async Task<ActionResult<LoginOtpCommandDto>> LoginWithOtp([FromQuery] LoginOtpCommand LoginOtpCommand, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(LoginOtpCommand);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("External-Login")]
    public async Task<ActionResult<GoogleLoginCommandDto>> ExternalLoginCallback([FromBody] GoogleLoginCommand loginCommand, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(loginCommand);
        return Ok(response);
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("Refresh-Token")]
    public async Task<ActionResult<RefreshTokenCommandDto>> RefreshToken([FromBody] RefreshTokenCommand refreshTokenCommand, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(refreshTokenCommand);
        return Ok(response);
    }

    [Produces("application/json")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(404)]
    [HttpPost("Update-2FA")]
    public async Task<ActionResult<TwoFAResponseDto>> Update2FA([FromQuery] Update2FACommand command, CancellationToken cancellationToken)
    {
        try
        {
            var response = await Mediator.Send(command, cancellationToken);
            return Ok(response);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Produces("application/json")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(404)]
    [HttpPost("Check-2FA")]
    public async Task<ActionResult<TwoFAResponseDto>> Check2FAStatus([FromQuery] Get2FAQuery command, CancellationToken cancellationToken)
    {
        try
        {
            var response = await Mediator.Send(command, cancellationToken);
            return Ok(response);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Produces("application/json")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(404)]
    [HttpPost("forget-password")]
    public async Task<ActionResult<ForgetPasswordDto>> ForgetPassword([FromQuery] ForgetPassword command, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [Produces("application/json")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(404)]
    [HttpPost("reset-password")]
    public async Task<ActionResult<ResetPasswordDto>> RestPassword([FromQuery] RestPassword restPasswordCommand, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(restPasswordCommand, cancellationToken);
        return Ok(response);
    }

    [HttpGet("ConfirmEmail")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<ActionResult<Response>> ConfirmEmail([FromQuery] ConfirmEmailCommand confirmEmailCommand, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(confirmEmailCommand);
        return Ok(response);
    }
}
