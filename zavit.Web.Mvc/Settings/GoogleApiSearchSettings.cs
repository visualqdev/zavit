using System.Configuration;
using zavit.Infrastructure.Places;

namespace zavit.Web.Mvc.Settings
{
    public class GoogleApiSearchSettings : IGoogleApiSearchSettings
    {
        string _placeSearchUri;
        public string PlaceSearchUri => _placeSearchUri ?? (_placeSearchUri = ConfigurationManager.AppSettings["Google.Api.PlaceSearchUri"]);

        string _serverKey;
        public string ServerKey => _serverKey ?? (_serverKey = ConfigurationManager.AppSettings["Google.Api.ServerKey"]);
    }
}