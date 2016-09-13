using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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

        public async Task<GooglePlaceSearchResult> NearbySearch(IPlaceSearchCriteria placeSearchCriteria)
        {
            var message = new HttpRequestMessage();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{_googleApiSearchSettings.PlaceUri}{NearbySearchPath}?key={_googleApiSearchSettings.ServerKey}&location={placeSearchCriteria.Latitude},{placeSearchCriteria.Longitude}&radius={placeSearchCriteria.Radius}");
            var httpResponse = await _httpClient.SendAsync(message);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = _jsonSerializer.Deserialize<GooglePlaceSearchResult>(json);
            return result;
        }

        public async Task<GooglePlaceDetailsResult> GetDetails(string placeId)
        {
            var message = new HttpRequestMessage();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{_googleApiSearchSettings.PlaceUri}{DetailsPath}?key={_googleApiSearchSettings.ServerKey}&placeid={placeId}");
            var httpResponse = await _httpClient.SendAsync(message);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = _jsonSerializer.Deserialize<GooglePlaceDetailsResult>(json);
            return result;
        }

        public async Task<GooglePlaceSearchResult> NearbySearchByName(IPlaceSearchByNameCriteria placeSearchByNameCriteria)
        {
            var message = new HttpRequestMessage();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{_googleApiSearchSettings.PlaceUri}{NearbySearchPath}?key={_googleApiSearchSettings.ServerKey}&location={placeSearchByNameCriteria.Latitude},{placeSearchByNameCriteria.Longitude}&radius={placeSearchByNameCriteria.Radius}&name={placeSearchByNameCriteria.Name}");
            var httpResponse = await _httpClient.SendAsync(message);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = _jsonSerializer.Deserialize<GooglePlaceSearchResult>(json);
            return result;
        }
    }
}