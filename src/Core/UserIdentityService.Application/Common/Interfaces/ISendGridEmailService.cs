using UserIdentityService.Application.Common.Models;

namespace UserIdentityService.Application.Common.Interfaces;

public interface ISendGridEmailService
{
    Task SendEmailAsync(EmailModel email, CancellationToken cancellationToken);

}
