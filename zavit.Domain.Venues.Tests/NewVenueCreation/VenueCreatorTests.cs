using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.Venues.NewVenueCreation;
using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Domain.Venues.Tests.NewVenueCreation 
{
    [Subject("VenueCreator")]
    public class VenueCreatorTests : TestOf<VenueCreator>
    {
        class When_creating_a_new_venue
        {
            Because of = () => _result = Subject.Create(_newVenue, _venueOwnerAccount).Result;

            It should_set_the_venue_name_to_be_the_same_as_the_name_specified_in_new_venue_request = 
                () => _result.Name.ShouldEqual(_newVenue.Name);

            It should_set_the_owner_of_the_new_venue_to_be_the_provided_venue_owner_account =
                () => _result.OwnerAccount.ShouldEqual(_venueOwnerAccount);

            It should_set_the_activities_to_the_activities_retrieved_from_repository_by_their_ids =
                () => _result.Activities.ShouldContainOnly(_activities);

            It should_set_the_place_id_to_be_the_place_id_of_the_public_place = () => _result.PublicPlaceId.ShouldEqual(_publicPlace.PlaceId);

            It should_set_the_address_to_be_the_address_of_the_public_place = () => _result.Address.ShouldEqual(_publicPlace.Address);

            It should_set_the_latitude_to_be_the_latitude_of_the_public_place = () => _result.Latitude.ShouldEqual(_publicPlace.Latitude);

            It should_set_the_longitude_to_be_the_longitude_of_the_public_place = () => _result.Longitude.ShouldEqual(_publicPlace.Longitude);

            Establish context = () =>
            {
                _venueOwnerAccount = NewInstanceOf<Account>();

                _newVenue = NewInstanceOf<NewVenue>();
                _newVenue.Name = "New venue name";
                _newVenue.PublicPlaceId = "PublicPlaceID";
                _newVenue.ActivityIds = new[] {1, 2};

                _activities = new[] { NewInstanceOf<Activity>(), NewInstanceOf<Activity>() };
                Injected<IActivityRepository>()
                    .Stub(r => r.GetActivities(_newVenue.ActivityIds))
                    .Return(_activities);

                _publicPlace = NewInstanceOf<PublicPlace>();
                _publicPlace.PlaceId = "Place ID";
                _publicPlace.Address = "Place address";
                _publicPlace.Name = "Place name";
                _publicPlace.Latitude = 0.1;
                _publicPlace.Longitude = -0.2;

                Injected<IPublicPlacesService>().Stub(s => s.GetPublicPlace(_newVenue.PublicPlaceId)).Return(Task.FromResult(_publicPlace));
            };

            static NewVenue _newVenue;
            static Venue _result;
            static Account _venueOwnerAccount;
            static Activity[] _activities;
            static PublicPlace _publicPlace;
        }

        class When_creating_a_new_venue_but_name_is_not_specified
        {
            Because of = () => _result = Subject.Create(_newVenue, _venueOwnerAccount).Result;

            It should_set_the_venue_name_to_be_the_same_as_the_public_place_name =
                () => _result.Name.ShouldEqual(_publicPlace.Name);

            It should_set_the_owner_of_the_new_venue_to_be_the_provided_venue_owner_account =
                () => _result.OwnerAccount.ShouldEqual(_venueOwnerAccount);

            It should_set_the_activities_to_the_activities_retrieved_from_repository_by_their_ids =
                () => _result.Activities.ShouldContainOnly(_activities);

            It should_set_the_place_id_to_be_the_place_id_of_the_public_place = () => _result.PublicPlaceId.ShouldEqual(_publicPlace.PlaceId);

            It should_set_the_address_to_be_the_address_of_the_public_place = () => _result.Address.ShouldEqual(_publicPlace.Address);

            It should_set_the_latitude_to_be_the_latitude_of_the_public_place = () => _result.Latitude.ShouldEqual(_publicPlace.Latitude);

            It should_set_the_longitude_to_be_the_longitude_of_the_public_place = () => _result.Longitude.ShouldEqual(_publicPlace.Longitude);

            Establish context = () =>
            {
                _venueOwnerAccount = NewInstanceOf<Account>();

                _newVenue = NewInstanceOf<NewVenue>();
                _newVenue.PublicPlaceId = "PublicPlaceID";
                _newVenue.ActivityIds = new[] { 1, 2 };

                _activities = new[] { NewInstanceOf<Activity>(), NewInstanceOf<Activity>() };
                Injected<IActivityRepository>()
                    .Stub(r => r.GetActivities(_newVenue.ActivityIds))
                    .Return(_activities);

                _publicPlace = NewInstanceOf<PublicPlace>();
                _publicPlace.PlaceId = "Place ID";
                _publicPlace.Address = "Place address";
                _publicPlace.Name = "Place name";
                _publicPlace.Latitude = 0.1;
                _publicPlace.Longitude = -0.2;

                Injected<IPublicPlacesService>().Stub(s => s.GetPublicPlace(_newVenue.PublicPlaceId)).Return(Task.FromResult(_publicPlace));
            };

            static NewVenue _newVenue;
            static Venue _result;
            static Account _venueOwnerAccount;
            static Activity[] _activities;
            static PublicPlace _publicPlace;
        }
    }
}

