using E_Learning.Application.Abstractions.Clock;

namespace E_Learning.Infrastructure.Clock
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
