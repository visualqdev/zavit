using System.Collections.Generic;

namespace zavit.Domain.Places
{
    public interface IPlaceService
    {
        IEnumerable<IPlace> Suggest();
    }
}