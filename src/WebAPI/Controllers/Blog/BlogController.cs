using UserIdentityService.API.Controllers.BaseController;
using Microsoft.AspNetCore.Mvc;
using UserIdentityService.Application.Common;
using UserIdentityService.Application.Handlers.Blogs;
using Microsoft.AspNetCore.Authorization;

namespace UserIdentityService.API.Controllers.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ApiController
    {
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(404)]
        [HttpPost("Create-Blog")]
        public async Task<ActionResult<Response>> CreateBlog([FromBody] CreateBlogCommand command, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }


        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(404)]
        [HttpPost("Update-Blog")]
        public async Task<ActionResult<Response>> UpdateBlog([FromBody] UpdateBlogCommand command, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet("get-blog-by-id")]
        public async Task<ActionResult<GetBlogResponse>> GetBlogById([FromBody] GetBlogCommand command, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
