using System.Collections.Generic;
using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using Rhino.Mspec.Contrib.Extensions;
using zavit.Domain.Accounts;
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
            Because of = () => _result = Subject.AddVenue(_venueDetailsDto).Result;

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
                Injected<IVenueService>().Stub(s => s.CreateVenue(newVenueRequest, venueOwnerAccount)).Return(Task.FromResult(_venue));

                _newVenueDetailsDto = NewInstanceOf<VenueDetailsDto>();
                Injected<IVenueDetailsDtoFactory>().Stub(f => f.Create(_venue)).Return(_newVenueDetailsDto);
            };

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
                Injected<IVenueService>().Stub(s => s.GetDefaultVenue(PlaceId)).Return(Task.FromResult(venue));

                _venueDetailsDto = NewInstanceOf<VenueDetailsDto>();
                Injected<IVenueDetailsDtoFactory>().Stub(f => f.Create(venue)).Return(_venueDetailsDto);
            };

            static VenueDetailsDto _result;
            static VenueDetailsDto _venueDetailsDto;
            const string PlaceId = "Place ID";
        }

        class When_getting_a_venue_details
        {
            Because of = () => _result = Subject.GetVenue(VenueId);

            It should_return_a_dto_created_from_the_venue = () => _result.ShouldEqual(_venueDetailsDto);

            Establish context = () =>
            {
                var venue = NewInstanceOf<Venue>();
                Injected<IVenueRepository>().Stub(r => r.GetVenue(VenueId)).Return(venue);

                _venueDetailsDto = NewInstanceOf<VenueDetailsDto>();
                Injected<IVenueDetailsDtoFactory>().Stub(f => f.Create(venue)).Return(_venueDetailsDto);
            };

            static VenueDetailsDto _result;
            static VenueDetailsDto _venueDetailsDto;
            const int VenueId = 123;
        }

        class When_suggesting_venue_dtos
        {
            Because of = () => _result = Subject.SuggestVenues(_venueSearchCriteriaDto).Result;

            It should_return_a_dto_for_each_of_the_venues_suggested_by_the_venue_service = () => _result.ShouldContainOnlyOrdered(_placeDto, _otherPlaceDto);

            Establish context = () =>
            {
                _venueSearchCriteriaDto = NewInstanceOf<VenueSearchCriteriaDto>();

                var venue = NewInstanceOf<IVenue>();
                var otherVenue = NewInstanceOf<IVenue>();

                Injected<IVenueService>()
                    .Stub(s => s.SuggestVenues(_venueSearchCriteriaDto))
                    .Return(Task.FromResult<IEnumerable<IVenue>>(new[] { venue, otherVenue }));

                _placeDto = NewInstanceOf<VenueDto>();
                Injected<IVenueDtoFactory>().Stub(f => f.Create(venue)).Return(_placeDto);

                _otherPlaceDto = NewInstanceOf<VenueDto>();
                Injected<IVenueDtoFactory>().Stub(f => f.Create(otherVenue)).Return(_otherPlaceDto);
            };

            static IEnumerable<VenueDto> _result;
            static VenueSearchCriteriaDto _venueSearchCriteriaDto;
            static VenueDto _placeDto;
            static VenueDto _otherPlaceDto;
        }
    }
}

