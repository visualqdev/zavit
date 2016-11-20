using System.Collections.Generic;

namespace zavit.Domain.Shared.ResultCollections
{
    public interface IResultCollection<out T>
    {
        IEnumerable<T> Results { get; }
        int Take { get; }
        bool HasMoreResults { get; }
    }
}