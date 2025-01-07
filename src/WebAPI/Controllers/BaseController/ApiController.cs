using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserIdentityService.API.Controllers.BaseController;

[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}
