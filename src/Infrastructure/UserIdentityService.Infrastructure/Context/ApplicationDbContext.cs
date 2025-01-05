using UserIdentityService.Application.Interfaces;

namespace UserIdentityService.Infrastructure.Context;

public class ApplicationDbContext : IApplicationDbContext
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
