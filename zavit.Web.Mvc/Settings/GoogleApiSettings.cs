using System.Configuration;
using zavit.Infrastructure.Places;

namespace zavit.Web.Mvc.Settings
{
    public class GoogleApiSettings : IGoogleApiSettings
    {
        string _placeUri;
        public string PlaceUri => _placeUri ?? (_placeUri = ConfigurationManager.AppSettings["Google.Api.PlaceUri"]);

        string _serverKey;
        public string ServerKey => _serverKey ?? (_serverKey = ConfigurationManager.AppSettings["Google.Api.ServerKey"]);
    }
}