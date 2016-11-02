using System;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreadParticipants;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.Tests.DtoFactories.Messaging.Messages 
{
    [Subject("MessageDtoFactory")]
    public class MessageDtoFactoryTests : TestOf<MessageDtoFactory>
    {
        class When_creating_message_dto_from_message_info
        {
            Because of = () => _result = Subject.CreateItem(_messageInfo);

            It should_set_the_sent_on_date_to_be_the_same_as_message = () => _result.SentOn.ShouldEqual(_message.SentOn);

            It should_set_the_body_to_be_the_same_as_message = () => _result.Body.ShouldEqual(_message.Body);

            It should_set_the_id_to_be_the_same_as_message = () => _result.Id.ShouldEqual(_message.Id);

            It should_set_the_status_to_be_the_same_status_as_the_message_info =
                () => _result.Status.ShouldEqual(_messageInfo.Status.ToString());

            It should_set_the_sender_to_be_the_participant_dto_of_the_participant_that_has_sent_the_message =
                () => _result.Sender.ShouldEqual(_participantDto);

            Establish context = () =>
            {
                _messageInfo = NewInstanceOf<MessageInfo>();
                _message = NewInstanceOf<Message>();
                _message.Id = 123;
                _message.Body = "Test body";
                _message.SentOn = new DateTime(2016, 10, 15);

                _messageInfo.Message = _message;
                _messageInfo.Status = MessageStatus.Read;

                _participantDto = NewInstanceOf<ThreadParticipantDto>();
                Injected<IThreadParticipantDtoFactory>()
                    .Stub(f => f.CreateItem(_message.Sender))
                    .Return(_participantDto);
            };

            static Message _message;
            static MessageDto _result;
            static MessageInfo _messageInfo;
            static ThreadParticipantDto _participantDto;
        }

        class When_creating_message_dto_from_message
        {
            Because of = () => _result = Subject.CreateItem(_message);

            It should_set_the_sent_on_date_to_be_the_same_as_message = () => _result.SentOn.ShouldEqual(_message.SentOn);

            It should_set_the_body_to_be_the_same_as_message = () => _result.Body.ShouldEqual(_message.Body);

            It should_set_the_id_to_be_the_same_as_message = () => _result.Id.ShouldEqual(_message.Id);

            It should_set_the_has_been_read_flag_to_false =
                () => _result.Status.ShouldEqual(MessageStatus.Sent.ToString());

            It should_set_the_sender_to_be_the_participant_dto_of_the_participant_that_has_sent_the_message =
                () => _result.Sender.ShouldEqual(_participantDto);

            Establish context = () =>
            {
                _message = NewInstanceOf<Message>();
                _message.Id = 123;
                _message.Body = "Test body";
                _message.SentOn = new DateTime(2016, 10, 15);
                _message.Sender = NewInstanceOf<Account>();

                _participantDto = NewInstanceOf<ThreadParticipantDto>();
                Injected<IThreadParticipantDtoFactory>()
                    .Stub(f => f.CreateItem(_message.Sender))
                    .Return(_participantDto);
            };

            static Message _message;
            static MessageDto _result;
            static ThreadParticipantDto _participantDto;
        }
    }
}

