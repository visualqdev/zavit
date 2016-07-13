using System;

namespace zavit.Web.Api.Dtos.Places
{
    public class PlaceDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PlaceId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}