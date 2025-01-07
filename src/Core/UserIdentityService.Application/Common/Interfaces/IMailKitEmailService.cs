using UserIdentityService.Application.Common.Services;

namespace UserIdentityService.Application.Common.Interfaces;

public interface IMailKitEmailService
{
    void SendEmail(Message message);
}
