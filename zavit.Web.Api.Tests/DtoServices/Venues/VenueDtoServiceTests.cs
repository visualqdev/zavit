using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Places;
using zavit.Domain.Venues;
using zavit.Domain.Venues.NewVenueCreation;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.Venues;
using zavit.Web.Api.DtoServices.Venues.NewVenues;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoServices.Venues 
{
    [Subject("VenueDtoServiceTests")]
    public class VenueDtoServiceTests : TestOf<VenueDtoService>
    {
        class When_adding_a_venue
        {
            Because of = () => _result = Subject.AddVenue(_venueDetailsDto, PlaceId).Result;

            It should_return_a_venue_details_dto_for_a_newly_created_venue = () => _result.ShouldEqual(_newVenueDetailsDto);

            Establish context = () =>
            {
                var venueOwnerAccount = NewInstanceOf<Account>();
                Injected<IUserContext>().Stub(c => c.Account).Return(venueOwnerAccount);

                _venueDetailsDto = NewInstanceOf<VenueDetailsDto>();

                var newVenueRequest = NewInstanceOf<NewVenue>();
                Injected<INewVenueProvider>()
                    .Stub(p => p.ProvideNewVenueRequest(_venueDetailsDto))
                    .Return(newVenueRequest);

                _venue = NewInstanceOf<Venue>();
                Injected<IPlaceService>().Stub(s => s.AddVenue(newVenueRequest, PlaceId, venueOwnerAccount)).Return(Task.FromResult(_venue));

                _newVenueDetailsDto = NewInstanceOf<VenueDetailsDto>();
                Injected<IVenueDetailsDtoFactory>().Stub(f => f.Create(_venue)).Return(_newVenueDetailsDto);
            };

            const string PlaceId = "Place ID";
            static VenueDetailsDto _result;
            static VenueDetailsDto _venueDetailsDto;
            static Venue _venue;
            static VenueDetailsDto _newVenueDetailsDto;
        }

        class When_getting_a_default_venue_details_by_place_id
        {
            Because of = () => _result = Subject.GetDefaultVenue(PlaceId).Result;

            It should_return_a_venue_details_dto_for_a_default_venue = () => _result.ShouldEqual(_venueDetailsDto);

            Establish context = () =>
            {
                var venue = NewInstanceOf<Venue>();
                Injected<IPlaceService>().Stub(s => s.GetDefaultVenue(PlaceId)).Return(Task.FromResult(venue));

                _venueDetailsDto = NewInstanceOf<VenueDetailsDto>();
                Injected<IVenueDetailsDtoFactory>().Stub(f => f.Create(venue)).Return(_venueDetailsDto);
            };

            static VenueDetailsDto _result;
            static VenueDetailsDto _venueDetailsDto;
            const string PlaceId = "Place ID";
        }
    }
}

