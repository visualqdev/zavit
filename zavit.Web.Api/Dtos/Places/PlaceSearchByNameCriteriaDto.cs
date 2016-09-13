using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zavit.Domain.Places.Search;

namespace zavit.Web.Api.Dtos.Places
{
    public class PlaceSearchByNameCriteriaDto : IPlaceSearchByNameCriteria
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Radius { get; set; }
        public string Name { get; set; }
    }
}
