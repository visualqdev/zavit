using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.VenueMemberships;
using zavit.Domain.Venues;
using zavit.Web.Api.DtoFactories.VenueMemberships;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Api.DtoServices.VenueMemberships;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoServices.VenueMemberships 
{
    [Subject("VenueMembershipDetailsDtoService")]
    public class VenueMembershipDetailsDtoServiceTests : TestOf<VenueMembershipDetailsDtoService>
    {
        class When_getting_a_venue_membership_details_dto_for_the_current_user_and_he_is_a_member_of_the_sepcified_venue
        {
            Because of = () => _result = Subject.GetMembershipDetails(VenueId);

            It should_return_the_new_dto_created_for_the_membership_of_the_current_user = () => _result.ShouldEqual(_venueMembershipDetailsDto);

            Establish context = () =>
            {
                _venueMembershipDetailsDto = NewInstanceOf<VenueMembershipDetailsDto>();

                var account = NewInstanceOf<Account>();
                Injected<IUserContext>().Stub(c => c.Account).Return(account);

                var venueMembership = NewInstanceOf<VenueMembership>();
                Injected<IVenueMembershipService>()
                    .Stub(s => s.GetVenueMembership(account, VenueId))
                    .Return(venueMembership);

                Injected<IVenueMembershipDetailsDtoFactory>()
                    .Stub(f => f.CreateItem(venueMembership))
                    .Return(_venueMembershipDetailsDto);
            };

            static VenueMembershipDetailsDto _result;
            static VenueMembershipDetailsDto _venueMembershipDetailsDto;
            const int VenueId = 123;
        }

        class When_getting_a_venue_membership_details_dto_for_the_current_user_and_he_is_not_a_member_of_the_sepcified_venue
        {
            Because of = () => _result = Subject.GetMembershipDetails(VenueId);

            It should_return_the_new_dto_created_for_the_specified_venue = () => _result.ShouldEqual(_venueMembershipDetailsDto);

            Establish context = () =>
            {
                _venueMembershipDetailsDto = NewInstanceOf<VenueMembershipDetailsDto>();

                var account = NewInstanceOf<Account>();
                Injected<IUserContext>().Stub(c => c.Account).Return(account);

                var venueMembership = NewInstanceOf<VenueMembership>();
                Injected<IVenueMembershipService>()
                    .Stub(s => s.GetVenueMembership(account, VenueId))
                    .Return(null);

                var venue = NewInstanceOf<Venue>();
                Injected<IVenueRepository>().Stub(s => s.GetVenue(VenueId)).Return(venue);

                Injected<IVenueMembershipDetailsDtoFactory>()
                    .Stub(f => f.CreateItem(venue))
                    .Return(_venueMembershipDetailsDto);
            };

            static VenueMembershipDetailsDto _result;
            static VenueMembershipDetailsDto _venueMembershipDetailsDto;
            const int VenueId = 123;
        }

        class When_getting_a_venue_membership_details_dto_for_the_current_user_and_he_is_a_member_of_the_venue_at_specified_place
        {
            Because of = () => _result = Subject.GetMembershipDetails(PublicPlaceId).Result;

            It should_return_the_new_dto_created_for_the_default_venue_at_the_specified_public_place = () => _result.ShouldEqual(_venueMembershipDetailsDto);

            Establish context = () =>
            {
                _venueMembershipDetailsDto = NewInstanceOf<VenueMembershipDetailsDto>();

                var account = NewInstanceOf<Account>();
                Injected<IUserContext>().Stub(c => c.Account).Return(account);

                Injected<IVenueMembershipService>()
                    .Stub(s => s.GetVenueMembership(account, PublicPlaceId))
                    .Return(null);

                var venue = NewInstanceOf<Venue>();
                Injected<IVenueService>().Stub(s => s.GetDefaultVenue(PublicPlaceId)).Return(Task.FromResult(venue));

                Injected<IVenueMembershipDetailsDtoFactory>()
                    .Stub(f => f.CreateItem(venue))
                    .Return(_venueMembershipDetailsDto);
            };

            static VenueMembershipDetailsDto _result;
            static VenueMembershipDetailsDto _venueMembershipDetailsDto;
            const string PublicPlaceId = "Public Place ID";
        }

        class When_getting_a_venue_membership_details_dto_for_the_current_user_and_he_is_not_a_member_of_the_venue_at_specified_place
        {
            Because of = () => _result = Subject.GetMembershipDetails(PublicPlaceId).Result;

            It should_return_the_new_dto_created_for_membership_registered_at_the_specified_public_place = () => _result.ShouldEqual(_venueMembershipDetailsDto);

            Establish context = () =>
            {
                _venueMembershipDetailsDto = NewInstanceOf<VenueMembershipDetailsDto>();

                var account = NewInstanceOf<Account>();
                Injected<IUserContext>().Stub(c => c.Account).Return(account);

                var venueMembership = NewInstanceOf<VenueMembership>();
                Injected<IVenueMembershipService>()
                    .Stub(s => s.GetVenueMembership(account, PublicPlaceId))
                    .Return(venueMembership);

                Injected<IVenueMembershipDetailsDtoFactory>()
                    .Stub(f => f.CreateItem(venueMembership))
                    .Return(_venueMembershipDetailsDto);
            };

            static VenueMembershipDetailsDto _result;
            static VenueMembershipDetailsDto _venueMembershipDetailsDto;
            const string PublicPlaceId = "Public Place ID";
        }
    }
}

