﻿using System.Collections.Generic;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;
using zavit.Infrastructure.Places.PublicPlacesApis;
using zavit.Infrastructure.Places.PublicPlacesApis.Details;
using zavit.Infrastructure.Places.PublicPlacesApis.Search;

namespace zavit.Infrastructure.Places
{
    public class PublicPlacesService : IPublicPlacesService
    {
        readonly IGooglePlacesApi _googlePlacesApi;
        readonly IPlaceSearchResultsTransformer _publicPlacesTransformer;
        readonly IPlaceDetailsResultTransformer _placeDetailsResultTransformer;

        public PublicPlacesService(IGooglePlacesApi googlePlacesApi, IPlaceSearchResultsTransformer publicPlacesTransformer, IPlaceDetailsResultTransformer placeDetailsResultTransformer)
        {
            _googlePlacesApi = googlePlacesApi;
            _publicPlacesTransformer = publicPlacesTransformer;
            _placeDetailsResultTransformer = placeDetailsResultTransformer;
        }

        public IEnumerable<PublicPlace> GetPublicPlaces(IPlaceSearchCriteria placeSearchCriteria)
        {
            var searchResult = _googlePlacesApi.NearbySearch(placeSearchCriteria);
            var publicPlaces = _publicPlacesTransformer.Transform(searchResult);

            return publicPlaces;
        }

        public PublicPlace GetPublicPlace(string placeId)
        {
            var placeDetailsResult = _googlePlacesApi.GetDetails(placeId);
            var publicPlace = _placeDetailsResultTransformer.Transform(placeDetailsResult);

            return publicPlace;
        }
    }
}