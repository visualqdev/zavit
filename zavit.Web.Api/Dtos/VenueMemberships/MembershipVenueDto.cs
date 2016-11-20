namespace zavit.Web.Api.Dtos.VenueMemberships
{
    public class MembershipVenueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}