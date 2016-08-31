using System;

namespace zavit.Domain.Shared
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}