using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Infrastructure.Persistence;
using UserIdentityService.Infrastructure.Services.EmailService;
using UserIdentityService.Application.Common.Models;
using UserIdentityService.Infrastructure.Services;

namespace UserIdentityService.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext
        var connectionString = configuration.GetConnectionString("UserConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        //Add Email Services
        //services.AddSendGrid(configuration);
        services.AddEmailServices(configuration);
        // Add Config for Required Email
        services.Configure<IdentityOptions>(opts => opts.SignIn.RequireConfirmedEmail = true);
        services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(10));

        //services.AddIdentityApiEndpoints<RegisterUser>().AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<ITokenGenerator, TokenGenerator>();

        return services;
    }
    //private static void AddSendGrid(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddSendGrid(options =>
    //    {
    //        options.ApiKey = configuration["Email:Key"];
    //        options.ReliabilitySettings = new ReliabilitySettings(1, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(3));
    //    });
    //    services.AddScoped<ISendGridEmailService, SendGridEmailService>();
    //}

    private static void AddEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfigurationModel>();
        services.AddSingleton(emailConfig);
        services.AddScoped<IMailKitEmailService, MailKitEmailService>();
    }
}
