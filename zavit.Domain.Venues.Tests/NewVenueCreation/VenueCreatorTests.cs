using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Venues.Tests.NewVenueCreation 
{
    [Subject("VenueCreator")]
    public class VenueCreatorTests : TestOf<VenueCreator>
    {
        class When_creating_a_new_venue
        {
            Because of = () => _result = Subject.Create(_newVenue, _venueOwnerAccount);

            It should_set_the_venue_name_to_be_the_same_as_the_name_specified_in_new_venue_request = 
                () => _result.Name.ShouldEqual(_newVenue.Name);

            It should_set_the_owner_of_the_new_venue_to_be_the_provided_venue_owner_account =
                () => _result.OwnerAccount.ShouldEqual(_venueOwnerAccount);

            It should_set_the_activities_to_the_activities_retrieved_from_repository_by_their_ids =
                () => _result.Activities.ShouldContainOnly(_activities);

            Establish context = () =>
            {
                _venueOwnerAccount = NewInstanceOf<Account>();

                _newVenue = NewInstanceOf<NewVenue>();
                _newVenue.Name = "New venue name";
                _newVenue.ActivityIds = new[] {1, 2};

                _activities = new[] { NewInstanceOf<Activity>(), NewInstanceOf<Activity>() };
                Injected<IActivityRepository>()
                    .Stub(r => r.GetActivities(_newVenue.ActivityIds))
                    .Return(_activities);
            };

            static NewVenue _newVenue;
            static Venue _result;
            static Account _venueOwnerAccount;
            static Activity[] _activities;
        }
    }
}

