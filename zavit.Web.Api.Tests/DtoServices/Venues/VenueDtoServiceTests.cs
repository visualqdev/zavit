using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.Venues;

namespace zavit.Web.Api.Tests.DtoServices.Venues 
{
    [Subject("VenueDtoServiceTests")]
    public class VenueDtoServiceTests : TestOf<VenueDtoService>
    {
        class When_adding_a_venue
        {
            Because of = () => _result = Subject.AddVenue(_venueDto, PlaceId);

            It should_ = () => ;

            Establish context = () =>
            {
                _venueDto = NewInstanceOf<VenueDto>();
                Injected<IVenueService>().Stub(v => CreateVenue(_venueDto, PlaceId)).Return(_venue);
            };

            const string PlaceId = "Place ID";
            static VenueDto _result;
            static VenueDto _venueDto;
            static Venue _venue;
        }
    }
}

