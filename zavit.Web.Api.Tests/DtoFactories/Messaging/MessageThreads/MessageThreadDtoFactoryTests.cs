using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreadParticipants;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.Tests.DtoFactories.Messaging.MessageThreads 
{
    [Subject("MessageThreadDtoFactory")]
    public class MessageThreadDtoFactoryTests : TestOf<MessageThreadDtoFactory>
    {
        class When_creating_message_thread_dto
        {
            Because of = () => _result = Subject.CreateItem(_messageThread);

            It should_set_the_id_to_be_the_id_of_the_message_thread = () => _result.ThreadId.ShouldEqual(_messageThread.Id);

            It should_create_a_participant_dto_for_each_participant_on_the_message_thread = () => _result.Participants.ShouldContainOnly(_participantDto, _otherParticipantDto);

            Establish context = () =>
            {
                _messageThread = NewInstanceOf<MessageThread>();
                _messageThread.Id = 123;

                var participant = NewInstanceOf<Account>();
                var otherParticipant = NewInstanceOf<Account>();
                _messageThread.Participants = new List<Account> { participant, otherParticipant };

                _participantDto = NewInstanceOf<ThreadParticipantDto>();
                Injected<IThreadParticipantDtoFactory>().Stub(p => p.CreateItem(participant)).Return(_participantDto);

                _otherParticipantDto = NewInstanceOf<ThreadParticipantDto>();
                Injected<IThreadParticipantDtoFactory>().Stub(p => p.CreateItem(otherParticipant)).Return(_otherParticipantDto);
            };

            static MessageThread _messageThread;
            static MessageThreadDto _result;
            static ThreadParticipantDto _otherParticipantDto;
            static ThreadParticipantDto _participantDto;
        }
    }
}

