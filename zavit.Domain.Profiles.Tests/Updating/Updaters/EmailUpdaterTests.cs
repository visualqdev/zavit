using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Profiles.Updating;
using zavit.Domain.Profiles.Updating.Updaters;

namespace zavit.Domain.Profiles.Tests.Updating.Updaters 
{
    [Subject("EmailUpdater")]
    public class EmailUpdaterTests : TestOf<EmailUpdater>
    {
        class When_updating_email_to_a_new_value
        {
            Because of = () => _result = Subject.Update(_profile, _profileUpdate);

            It should_set_the_display_name_to_the_new_value = () => _profile.Email.ShouldEqual(_profileUpdate.Email);

            It should_return_true_to_indicate_that_the_profile_has_been_updated = () => _result.ShouldBeTrue();

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
                _profile.Email = "test@email.com";

                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.Email = "othertest@email.com";
            };

            static ProfileUpdate _profileUpdate;
            static Profile _profile;
            static bool _result;
        }

        class When_updating_email_but_the_provided_value_is_the_same
        {
            Because of = () => _result = Subject.Update(_profile, _profileUpdate);

            It should_return_false_to_indicate_that_the_profile_has_not_been_updated = () => _result.ShouldBeFalse();

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
                _profile.Email = "test@email.com";

                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.Email = _profile.Email;
            };

            static ProfileUpdate _profileUpdate;
            static Profile _profile;
            static bool _result;
        }

        class When_updating_email_but_the_provided_value_not_set
        {
            Because of = () => _result = Subject.Update(_profile, _profileUpdate);

            It should_keep_the_current_email_address = () => _profile.Email.ShouldEqual(CurrentEmailAddress);

            It should_return_false_to_indicate_that_the_profile_has_not_been_updated = () => _result.ShouldBeFalse();

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
                _profile.Email = CurrentEmailAddress;

                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.DisplayName = "";
            };

            static ProfileUpdate _profileUpdate;
            static Profile _profile;
            static bool _result;
            const string CurrentEmailAddress = "test@email.com";
        }
    }
}

