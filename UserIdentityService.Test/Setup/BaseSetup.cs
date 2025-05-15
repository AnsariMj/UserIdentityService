using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserIdentityService.Infrastructure.Persistence;

namespace UserIdentityService.Test.Setup;

public abstract class BaseSetup
{
    protected static int _dbCount = 1;

    protected DbContextOptionsBuilder<ApplicationDbContext> _dbContextBuilder;
    protected UserManager<User>

}
