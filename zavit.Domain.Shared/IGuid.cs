using System;

namespace zavit.Domain.Shared
{
    public interface IGuid
    {
        Guid NewGuid();
        string NewGuidString(string format = "N");
    }
}