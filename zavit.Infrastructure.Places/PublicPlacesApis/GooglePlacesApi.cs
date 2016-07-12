using System;
using System.Net.Http;
using System.Net.Http.Headers;
using zavit.Domain.Places.Search;
using zavit.Infrastructure.Core.Serialization;
using zavit.Infrastructure.Places.PublicPlacesApis.Details;
using zavit.Infrastructure.Places.PublicPlacesApis.Search;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public class GooglePlacesApi : IGooglePlacesApi
    {
        const string NearbySearchPath = "/nearbysearch/json";
        const string DetailsPath = "/details/json";

        readonly IGoogleApiSettings _googleApiSearchSettings;
        readonly IJsonSerializer _jsonSerializer;
        readonly HttpClient _httpClient;

        public GooglePlacesApi(IGoogleApiSettings googleApiSearchSettings, IJsonSerializer jsonSerializer)
        {
            _googleApiSearchSettings = googleApiSearchSettings;
            _jsonSerializer = jsonSerializer;
            _httpClient = new HttpClient();
        }

        public GooglePlaceSearchResult NearbySearch(IPlaceSearchCriteria placeSearchCriteria)
        {
            var message = new HttpRequestMessage();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{_googleApiSearchSettings.PlaceUri}{NearbySearchPath}?key={_googleApiSearchSettings.ServerKey}&location={placeSearchCriteria.Latitude},{placeSearchCriteria.Longitude}&radius={placeSearchCriteria.Radius}");
            var httpResponse = _httpClient.SendAsync(message).Result;
            var json = httpResponse.Content.ReadAsStringAsync();
            var result = _jsonSerializer.Deserialize<GooglePlaceSearchResult>(json.Result);
            return result;
        }

        public GooglePlaceDetailsResult GetDetails(string placeId)
        {
            var message = new HttpRequestMessage();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{_googleApiSearchSettings.PlaceUri}{DetailsPath}?key={_googleApiSearchSettings.ServerKey}&placeid={placeId}");
            var httpResponse = _httpClient.SendAsync(message).Result;
            var json = httpResponse.Content.ReadAsStringAsync();
            var result = _jsonSerializer.Deserialize<GooglePlaceDetailsResult>(json.Result);
            return result;
        }
    }
}