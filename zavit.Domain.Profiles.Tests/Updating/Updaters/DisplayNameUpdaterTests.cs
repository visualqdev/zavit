using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles.Updating;
using zavit.Domain.Profiles.Updating.Updaters;

namespace zavit.Domain.Profiles.Tests.Updating.Updaters 
{
    [Subject("DisplayNameUpdater")]
    public class DisplayNameUpdaterTests : TestOf<DisplayNameUpdater>
    {
        class When_updating_display_name_to_a_new_value
        {
            Because of = () => _result = Subject.Update(_profile, _profileUpdate);

            It should_set_the_display_name_to_the_new_value = () => _profile.Account.DisplayName.ShouldEqual(_profileUpdate.DisplayName);

            It should_return_true_to_indicate_that_the_profile_has_been_updated = () => _result.ShouldBeTrue();

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
                _profile.Account = NewInstanceOf<Account>();
                _profile.Account.DisplayName = "New display name";

                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.DisplayName = "Old display name";
            };

            static ProfileUpdate _profileUpdate;
            static Profile _profile;
            static bool _result;
        }

        class When_updating_display_name_but_the_provided_value_is_the_same
        {
            Because of = () => _result = Subject.Update(_profile, _profileUpdate);

            It should_return_false_to_indicate_that_the_profile_has_not_been_updated = () => _result.ShouldBeFalse();

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
                _profile.Account = NewInstanceOf<Account>();
                _profile.Account.DisplayName = "Same display name";

                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.DisplayName = _profile.Account.DisplayName;
            };

            static ProfileUpdate _profileUpdate;
            static Profile _profile;
            static bool _result;
        }

        class When_updating_display_name_but_the_provided_value_not_set
        {
            Because of = () => _result = Subject.Update(_profile, _profileUpdate);

            It should_keep_the_current_display_name_value = () => _profile.Account.DisplayName.ShouldEqual(CurrentDisplayName);

            It should_return_false_to_indicate_that_the_profile_has_not_been_updated = () => _result.ShouldBeFalse();

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
                _profile.Account = NewInstanceOf<Account>();
                _profile.Account.DisplayName = CurrentDisplayName;

                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.DisplayName = "";
            };

            static ProfileUpdate _profileUpdate;
            static Profile _profile;
            static bool _result;
            const string CurrentDisplayName = "Same display name";
        }
    }
}

