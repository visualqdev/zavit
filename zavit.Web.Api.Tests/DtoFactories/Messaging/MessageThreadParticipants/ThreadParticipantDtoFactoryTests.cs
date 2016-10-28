using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreadParticipants;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.Tests.DtoFactories.Messaging.MessageThreadParticipants 
{
    [Subject("ThreadParticipantDtoFactory")]
    public class ThreadParticipantDtoFactoryTests : TestOf<ThreadParticipantDtoFactory>
    {
        class When_creating_thread_participant_dto
        {
            Because of = () => _result = Subject.CreateItem(_account);

            It should_set_the_id_to_be_the_id_of_the_particpants_account = () => _result.AccountId.ShouldEqual(_account.Id);

            It should_set_the_display_name_to_be_the_account_disply_name = () => _result.DisplayName.ShouldEqual(_account.DisplayName);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;
                _account.DisplayName = "Test display name";
            };

            static Account _account;
            static ThreadParticipantDto _result;
        }
    }
}

