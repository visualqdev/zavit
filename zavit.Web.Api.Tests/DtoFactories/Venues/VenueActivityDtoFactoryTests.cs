using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Activities;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.Tests.DtoFactories.Venues 
{
    [Subject("VenueActivityDtoFactory")]
    public class VenueActivityDtoFactoryTests : TestOf<VenueActivityDtoFactory>
    {
        class When_creating_a_venue_activity_dto
        {
            Because of = () => _result = Subject.CreateItem(_activity);

            It should_set_the_name_to_be_the_activity_name = () => _result.Name.ShouldEqual(_activity.Name);

            It should_set_the_id_to_be_the_activity_id = () => _result.Id.ShouldEqual(_activity.Id);

            Establish context = () =>
            {
                _activity = NewInstanceOf<Activity>();
                _activity.Name = "Test activity name";
                _activity.Id = 123;
            };

            static VenueActivityDto _result;
            static Activity _activity;
        }
    }
}

