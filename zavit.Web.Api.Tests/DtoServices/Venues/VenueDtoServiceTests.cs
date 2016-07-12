using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places;
using zavit.Domain.Venues;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.Venues;

namespace zavit.Web.Api.Tests.DtoServices.Venues 
{
    [Subject("VenueDtoServiceTests")]
    public class VenueDtoServiceTests : TestOf<VenueDtoService>
    {
        class When_adding_a_venue
        {
            Because of = () => _result = Subject.AddVenue(_venueDto, PlaceId).Result;

            It should_return_a_venue_dto_for_a_newly_created_venue = () => _result.ShouldEqual(_newVenueDto);

            Establish context = () =>
            {
                _venueDto = NewInstanceOf<VenueDto>();
                _venue = NewInstanceOf<Venue>();
                Injected<IPlaceService>().Stub(s => s.AddVenue(_venueDto, PlaceId)).Return(Task.FromResult(_venue));

                _newVenueDto = NewInstanceOf<VenueDto>();
                Injected<IVenueDtoFactory>().Stub(f => f.Create(_venue)).Return(_newVenueDto);
            };

            const string PlaceId = "Place ID";
            static VenueDto _result;
            static VenueDto _venueDto;
            static Venue _venue;
            static VenueDto _newVenueDto;
        }
    }
}

