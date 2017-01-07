using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles;
using zavit.Domain.Profiles.Updating;
using zavit.Web.Api.DtoFactories.Profiles;
using zavit.Web.Api.Dtos.Profiles;
using zavit.Web.Api.DtoServices.Profiles;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoServices.Profiles 
{
    [Subject("ProfileDtoService")]
    public class ProfileDtoServiceTests : TestOf<ProfileDtoService>
    {
        class When_updating_a_profile
        {
            Because of = () => _result = Subject.Update(_profileDto);

            It should_return_dto_created_from_updated_profile = () => _result.ShouldEqual(_updatedProfileDto);

            Establish context = () =>
            {
                _profileDto = NewInstanceOf<ProfileDto>();
                _updatedProfileDto = NewInstanceOf<ProfileDto>();

                var profileUpdate = NewInstanceOf<ProfileUpdate>();
                Injected<IProfileUpdateFactory>().Stub(f => f.CreateItem(_profileDto)).Return(profileUpdate);

                var account = NewInstanceOf<Account>();
                account.Id = 123456;
                account.Profile = NewInstanceOf<Profile>();
                Injected<IUserContext>().Stub(c => c.Account).Return(account);

                var updatedProfile = NewInstanceOf<Profile>();
                Injected<IProfileService>().Stub(s => s.UpdateProfile(profileUpdate, account.Profile)).Return(updatedProfile);

                Injected<IProfileDtoFactory>().Stub(f => f.CreateItem(updatedProfile, account.Id)).Return(_updatedProfileDto);
            };

            static ProfileDto _profileDto;
            static ProfileDto _result;
            static ProfileDto _updatedProfileDto;
        }

        class When_getting_my_profile_dto
        {
            Because of = () => _result = Subject.GetMyProfile();

            It should_return_dto_created_from_my_profile = () => _result.ShouldEqual(_profileDto);

            Establish context = () =>
            {
                var account = NewInstanceOf<Account>();
                account.Id = 123456;
                account.Profile = NewInstanceOf<Profile>();
                Injected<IUserContext>().Stub(c => c.Account).Return(account);
                
                _profileDto = NewInstanceOf<ProfileDto>();
                Injected<IProfileDtoFactory>().Stub(f => f.CreateItem(account.Profile, account.Id)).Return(_profileDto);
            };

            static ProfileDto _result;
            static ProfileDto _profileDto;
        }
    }
}

