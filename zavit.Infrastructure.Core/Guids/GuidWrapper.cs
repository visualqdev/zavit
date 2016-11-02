using System;
using zavit.Domain.Shared;

namespace zavit.Infrastructure.Core.Guids
{
    public class GuidWrapper : IGuid
    {
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}