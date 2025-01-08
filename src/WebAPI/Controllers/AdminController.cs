using UserIdentityService.API.Controllers.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserIdentityService.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ApiController
    {
        [HttpGet("Users")]
        public IEnumerable<string> Get()
        {
            return new List<string> { "Mohammad", "Jilani", "Ansari" };
        }
    }
}
