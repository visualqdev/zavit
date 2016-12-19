using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles.Tests 
{
    [Subject("Profile")]
    public class ProfileTests : TestOf<Profile>
    {
        class When_accpeting_update_that_any_of_the_updaters_can_process
        {
            Because of = () => _result = Subject.AcceptUpdate(_profileUpdate, _profileUpdaters);

            It should_pass_the_profile_update_to_all_updaters = 
                () => _profileUpdaters.ForEach(u => u.AssertWasCalled(pu => pu.Update(Subject, _profileUpdate)));

            It should_return_true_to_indicate_that_the_profile_has_been_updated = () => _result.ShouldBeTrue();

            Establish context = () =>
            {
                _profileUpdate = NewInstanceOf<ProfileUpdate>();

                var updater = NewInstanceOf<IProfileUpdater>();
                updater.Stub(u => u.Update(Subject, _profileUpdate)).Return(false);

                var otherUpdater = NewInstanceOf<IProfileUpdater>();
                otherUpdater.Stub(u => u.Update(Subject, _profileUpdate)).Return(true);

                _profileUpdaters = new List<IProfileUpdater> { updater, otherUpdater };
            };

            static bool _result;
            static ProfileUpdate _profileUpdate;
            static List<IProfileUpdater> _profileUpdaters;
        }

        class When_accpeting_update_that_none_of_the_updaters_can_process
        {
            Because of = () => _result = Subject.AcceptUpdate(_profileUpdate, _profileUpdaters);

            It should_pass_the_profile_update_to_all_updaters =
                () => _profileUpdaters.ForEach(u => u.AssertWasCalled(pu => pu.Update(Subject, _profileUpdate)));

            It should_return_false_to_indicate_that_the_profile_has_not_been_updated = () => _result.ShouldBeFalse();

            Establish context = () =>
            {
                _profileUpdate = NewInstanceOf<ProfileUpdate>();

                var updater = NewInstanceOf<IProfileUpdater>();
                updater.Stub(u => u.Update(Subject, _profileUpdate)).Return(false);

                var otherUpdater = NewInstanceOf<IProfileUpdater>();
                otherUpdater.Stub(u => u.Update(Subject, _profileUpdate)).Return(false);

                _profileUpdaters = new List<IProfileUpdater> { updater, otherUpdater };
            };

            static bool _result;
            static ProfileUpdate _profileUpdate;
            static List<IProfileUpdater> _profileUpdaters;
        }
    }
}

