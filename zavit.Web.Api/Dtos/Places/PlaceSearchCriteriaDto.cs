﻿using zavit.Domain.Places.Search;

namespace zavit.Web.Api.Dtos.Places
{
    public class PlaceSearchCriteriaDto : IPlaceSearchCriteria
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Radius { get; set; }
    }
}