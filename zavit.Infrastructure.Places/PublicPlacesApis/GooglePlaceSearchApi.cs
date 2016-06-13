using System;
using System.Net.Http;
using System.Net.Http.Headers;
using zavit.Domain.Places.Search;
using zavit.Infrastructure.Core.Serialization;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public class GooglePlaceSearchApi : IGooglePlaceSearchApi
    {
        readonly IGoogleApiSearchSettings _googleApiSearchSettings;
        readonly IJsonSerializer _jsonSerializer;
        readonly HttpClient _httpClient;

        public GooglePlaceSearchApi(IGoogleApiSearchSettings googleApiSearchSettings, IJsonSerializer jsonSerializer)
        {
            _googleApiSearchSettings = googleApiSearchSettings;
            _jsonSerializer = jsonSerializer;
            _httpClient = new HttpClient();
        }

        public GooglePlacesSearchResult NearbySearch(IPlaceSearchCriteria placeSearchCriteria)
        {
            var message = new HttpRequestMessage();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{_googleApiSearchSettings.PlaceSearchUri}?key={_googleApiSearchSettings.ServerKey}&location={placeSearchCriteria.Latitude},{placeSearchCriteria.Longitude}&radius={placeSearchCriteria.Radius}");
            var httpResponse = _httpClient.SendAsync(message).Result;
            var json = httpResponse.Content.ReadAsStringAsync();
            var result = _jsonSerializer.Deserialize<GooglePlacesSearchResult>(json.Result);
            return result;
        }
    }
}