using Microsoft.Extensions.DependencyInjection;

namespace UserIdentityService.Application.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }

}
