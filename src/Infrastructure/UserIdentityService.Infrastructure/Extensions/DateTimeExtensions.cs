using UserIdentityService.Application.Common.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UserIdentityService.Infrastructure.Extensions;

public class DateTimeExtensions : IDateTimeExtension
{
    public DateTime ConvertToLocal(DateTime? sendDate, double timeoffset)
    {
        return TimeZoneInfo.ConvertTimeFromUtc((DateTime)sendDate, TimeZoneInfo.CreateCustomTimeZone("cus", TimeSpan.FromMinutes(timeoffset * -1), "cus", "cus"));

    }

    public DateTime ConvertToUtc(DateTime localDate, double timeZoneOffset)
    {
        return TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(localDate, DateTimeKind.Unspecified), TimeZoneInfo.CreateCustomTimeZone("cus", TimeSpan.FromMinutes(timeZoneOffset * -1), "cus", "cus"));
    }
}
