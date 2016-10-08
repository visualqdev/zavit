using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.VenueMemberships;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Api.DtoServices.VenueMemberships;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoServices.VenueMemberships 
{
    [Subject("VenueMembershipDetailsDtoService")]
    public class VenueMembershipDetailsDtoServiceTests : TestOf<VenueMembershipDetailsDtoService>
    {
        class When_getting_a_venue_membership_details_dto
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
    }
}

