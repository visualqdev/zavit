using Machine.Specifications.Model;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Venues;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.Tests.DtoFactories.Venues
{
    [Subject("VenueDtoFactory")]
    public class VenueDtoFactoryTests : TestOf<VenueDtoFactory>
    {
        class When_creating_a_new_dto
        {
            Because of = () => _result = Subject.Create(_venue);

            It should_set_the_dto_id_to_be_the_same_as_venue_id = () => _result.Id.ShouldEqual(_venue.Id);

            It should_set_the_name_to_be_the_same_as_the_name_of_the_venue = 
                () => _result.Name.ShouldEqual(_venue.Name);

            Establish context = () =>
            {
                _venue = NewInstanceOf<Venue>();
                _venue.Id = 1234;
                _venue.Name = "Venue name";
            };

            static Venue _venue;
            static VenueDto _result;
        }
    }
}