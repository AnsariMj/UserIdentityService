using UserIdentityService.Domain.Interfaces;

namespace UserIdentityService.Application.Common.Interfaces;

public interface IDateTimeExtension : IScopedService
{
    DateTime ConvertToLocal(DateTime? sendDate, double timeoffset);
    DateTime ConvertToUtc(DateTime localDate, double timeZoneOffset);
}
