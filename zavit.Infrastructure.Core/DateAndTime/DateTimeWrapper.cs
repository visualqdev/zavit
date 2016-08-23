using System;
using zavit.Domain.Shared;

namespace zavit.Infrastructure.Core.DateAndTime
{
    public class DateTimeWrapper : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}