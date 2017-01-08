using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.Profiles;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.VenueMembers;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.VenueMembers;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.Tests.DtoFactories.VenueMembers 
{
    [Subject("VenueMemberDtoFactory")]
    public class VenueMemberDtoFactoryTests : TestOf<VenueMemberDtoFactory>
    {
        class When_creating_a_venue_member_dto
        {
            Because of = () => _result = Subject.CreateItem(_venueMembership);

            It should_set_the_display_name_to_be_the_same_as_membership_account_dipslay_name = 
                () => _result.DisplayName.ShouldEqual(_venueMembership.Account.Profile.DisplayName);

            It should_set_the_account_id_to_be_the_same_as_membership_account_id =
                () => _result.AccountId.ShouldEqual(_venueMembership.Account.Id);

            It should_add_a_venue_activity_dto_for_each_membership_activity =
                () => _result.Activities.ShouldContainOnly(_activityDto, _otherActivityDto);

            It should_set_the_profile_image_url_to_be_the_url_provided_by_the_builder =
                () => _result.ProfileImageUrl.ShouldEqual(ProfileImageUrl);

            Establish context = () =>
            {
                _venueMembership = NewInstanceOf<VenueMembership>();
                _venueMembership.Account = NewInstanceOf<Account>();
                _venueMembership.Account.Id = 123;
                _venueMembership.Account.Profile = NewInstanceOf<Profile>();
                _venueMembership.Account.Profile.DisplayName = "Test display name";

                var activity = NewInstanceOf<Activity>();
                var otherActivity = NewInstanceOf<Activity>();
                _venueMembership.Activities = new[] { activity, otherActivity };

                _activityDto = NewInstanceOf<VenueActivityDto>();
                Injected<IVenueActivityDtoFactory>().Stub(f => f.CreateItem(activity)).Return(_activityDto);

                _otherActivityDto = NewInstanceOf<VenueActivityDto>();
                Injected<IVenueActivityDtoFactory>().Stub(f => f.CreateItem(otherActivity)).Return(_otherActivityDto);

                Injected<IProfileImageUrlBuilder>()
                    .Stub(b => b.Build(_venueMembership.Account.Profile))
                    .Return(ProfileImageUrl);
            };

            static VenueMembership _venueMembership;
            static VenueMemberDto _result;
            static VenueActivityDto _activityDto;
            static VenueActivityDto _otherActivityDto;
            const string ProfileImageUrl = "profile/image/url";
        }
    }
}

