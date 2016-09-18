using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Venues.NewVenueCreation;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.Venues.NewVenues;

namespace zavit.Web.Api.Tests.DtoServices.Venues.NewVenues 
{
    [Subject("NewVenueProvider")]
    public class NewVenueProviderTests : TestOf<NewVenueProvider>
    {
        class When_providing_a_new_venue_request
        {
            Because of = () => _result = Subject.ProvideNewVenueRequest(_venueDetailsDto);

            It should_set_the_name_of_the_new_venue_request_to_be_the_same_as_the_venue_details_dto =
                () => _result.Name.ShouldEqual(_venueDetailsDto.Name);

            It should_set_the_activity_ids_to_contain_ids_of_all_venue_activity_dtos =
                () => _result.ActivityIds.ShouldContainOnly(_activity.Id, _otherActivity.Id);

            Establish context = () =>
            {
                _venueDetailsDto = NewInstanceOf<VenueDetailsDto>();
                _venueDetailsDto.Name = "Test name";

                _activity = NewInstanceOf<VenueActivityDto>();
                _activity.Id = 123;
                _otherActivity = NewInstanceOf<VenueActivityDto>();
                _otherActivity.Id = 456;
                _venueDetailsDto.Activities = new[] { _activity, _otherActivity };
            };

            static VenueDetailsDto _venueDetailsDto;
            static NewVenue _result;
            static VenueActivityDto _otherActivity;
            static VenueActivityDto _activity;
        }
    }
}

