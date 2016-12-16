using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles.Tests 
{
    [Subject("ProfileService")]
    public class ProfileServiceTests : TestOf<ProfileService>
    {
        class When_updating_successfully_updating_profile
        {
            Because of = () => _result = Subject.Update(_profileUpdate);

            It should_save_the_updated_profile = () => Injected<IProfileRepository>().AssertWasCalled(r => r.Update(_profile));

            It should_return_the_updated_profile = () => _result.ShouldEqual(_profile);

            Establish context = () =>
            {
                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.Account = NewInstanceOf<Account>();
                _profileUpdate.Account.Id = 132;

                _profile = NewInstanceOf<Profile>();
                Injected<IProfileRepository>().Stub(r => r.GetForAccount(_profileUpdate.Account.Id)).Return(_profile);

                var updaters = (List<IProfileUpdater>) Injected<IEnumerable<IProfileUpdater>>();
                updaters.Add(NewInstanceOf<IProfileUpdater>());
                _profile.Stub(p => p.AcceptUpdate(_profileUpdate, updaters)).Return(true);
            };

            static ProfileUpdate _profileUpdate;
            static Profile _result;
            static Profile _profile;
        }

        class When_updating_profile_but_the_profile_does_not_accept_the_update
        {
            Because of = () => _result = Subject.Update(_profileUpdate);

            It should_not_try_to_save_the_profile = () => Injected<IProfileRepository>().AssertWasNotCalled(r => r.Update(_profile));

            It should_return_the_original_profile = () => _result.ShouldEqual(_profile);

            Establish context = () =>
            {
                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.Account = NewInstanceOf<Account>();
                _profileUpdate.Account.Id = 132;

                _profile = NewInstanceOf<Profile>();
                Injected<IProfileRepository>().Stub(r => r.GetForAccount(_profileUpdate.Account.Id)).Return(_profile);

                var updaters = (List<IProfileUpdater>)Injected<IEnumerable<IProfileUpdater>>();
                updaters.Add(NewInstanceOf<IProfileUpdater>());
                _profile.Stub(p => p.AcceptUpdate(_profileUpdate, updaters)).Return(false);
            };

            static ProfileUpdate _profileUpdate;
            static Profile _result;
            static Profile _profile;
        }
    }
}

