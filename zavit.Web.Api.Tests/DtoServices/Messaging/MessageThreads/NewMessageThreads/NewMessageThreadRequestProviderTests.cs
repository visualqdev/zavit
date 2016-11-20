using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessageThreads;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoServices.Messaging.MessageThreads.NewMessageThreads 
{
    [Subject("NewMessageThreadRequestProvider")]
    public class NewMessageThreadRequestProviderTests : TestOf<NewMessageThreadRequestProvider>
    {
        class When_providing_a_new_message_thread_request
        {
            Because of = () => _result = Subject.Provide(_messageThreadDto);

            It should_set_the_participant_ids_to_be_the_list_of_ids_of_all_participants_including_the_current_user = 
                () => _result.ParticipantIds.ShouldContainOnly(_threadParticipantDto.AccountId, _otherThreadParticipantDto.AccountId, _currentUserAccount.Id);

            Establish context = () =>
            {
                _currentUserAccount = NewInstanceOf<Account>();
                _currentUserAccount.Id = 20;
                Injected<IUserContext>().Stub(c => c.Account).Return(_currentUserAccount);

                _messageThreadDto = NewInstanceOf<MessageThreadDto>();

                _threadParticipantDto = NewInstanceOf<ThreadParticipantDto>();
                _threadParticipantDto.AccountId = 12;
                _otherThreadParticipantDto = NewInstanceOf<ThreadParticipantDto>();
                _otherThreadParticipantDto.AccountId = 14;
                _messageThreadDto.Participants = new[] { _threadParticipantDto, _otherThreadParticipantDto };
            };

            static MessageThreadDto _messageThreadDto;
            static NewMessageThreadRequest _result;
            static ThreadParticipantDto _threadParticipantDto;
            static ThreadParticipantDto _otherThreadParticipantDto;
            static Account _currentUserAccount;
        }

        class When_providing_a_new_message_thread_request_and_the_current_user_is_alread_listed_as_a_participant
        {
            Because of = () => _result = Subject.Provide(_messageThreadDto);

            It should_set_the_participant_ids_without_duplicating_the_current_user =
                () => _result.ParticipantIds.ShouldContainOnly(_threadParticipantDto.AccountId, _currentUserAccount.Id);

            Establish context = () =>
            {
                _currentUserAccount = NewInstanceOf<Account>();
                _currentUserAccount.Id = 20;
                Injected<IUserContext>().Stub(c => c.Account).Return(_currentUserAccount);

                _messageThreadDto = NewInstanceOf<MessageThreadDto>();

                _threadParticipantDto = NewInstanceOf<ThreadParticipantDto>();
                _threadParticipantDto.AccountId = 12;
                _otherThreadParticipantDto = NewInstanceOf<ThreadParticipantDto>();
                _otherThreadParticipantDto.AccountId = _currentUserAccount.Id;
                _messageThreadDto.Participants = new[] { _threadParticipantDto, _otherThreadParticipantDto };
            };

            static MessageThreadDto _messageThreadDto;
            static NewMessageThreadRequest _result;
            static ThreadParticipantDto _threadParticipantDto;
            static ThreadParticipantDto _otherThreadParticipantDto;
            static Account _currentUserAccount;
        }
    }
}

