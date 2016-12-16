using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Profiles.Updating;
using zavit.Domain.Profiles.Updating.Updaters;

namespace zavit.Domain.Profiles.Tests.Updating.Updaters 
{
    [Subject("GenderUpdater")]
    public class GenderUpdaterTests : TestOf<GenderUpdater>
    {
        class When_updating_gender_to_a_new_value
        {
            Because of = () => _result = Subject.Update(_profile, _profileUpdate);

            It should_set_the_gender_to_the_new_value = () => _profile.Gender.ShouldEqual(_profileUpdate.Gender);

            It should_return_true_to_indicate_that_the_profile_has_been_updated = () => _result.ShouldBeTrue();

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
                _profile.Gender = Gender.Male;

                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.Gender = Gender.Female;
            };

            static ProfileUpdate _profileUpdate;
            static Profile _profile;
            static bool _result;
        }

        class When_updating_gender_but_the_provided_value_is_the_same
        {
            Because of = () => _result = Subject.Update(_profile, _profileUpdate);

            It should_return_false_to_indicate_that_the_profile_has_not_been_updated = () => _result.ShouldBeFalse();

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
                _profile.Gender = Gender.Male;

                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.Gender = _profile.Gender;
            };

            static ProfileUpdate _profileUpdate;
            static Profile _profile;
            static bool _result;
        }
    }
}

