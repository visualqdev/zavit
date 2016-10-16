using System;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Shared;

namespace zavit.Domain.Messaging.Tests.Messages 
{
    [Subject("NewMessageProvider")]
    public class NewMessageProviderTests : TestOf<NewMessageProvider>
    {
        class When_providing_message
        {
            Because of = () => _result = Subject.Provide(_newMessageRequest, _messageThread);

            It should_set_the_message_body_to_match_the_request = () => _result.Body.ShouldEqual(_newMessageRequest.Body);

            It should_set_the_message_thread_to_the_provided_message_thread =
                () => _result.MessageThread.ShouldEqual(_messageThread);

            It should_set_the_sender_to_be_the_sender_from_the_request = () => _result.Sender.ShouldEqual(_newMessageRequest.Sender);

            It should_set_the_sent_on_to_be_the_current_utc_date_and_time = () => _result.SentOn.ShouldEqual(Injected<IDateTime>().UtcNow);

            Establish context = () =>
            {
                _newMessageRequest = NewInstanceOf<NewMessageRequest>();
                _newMessageRequest.Body = "Test body";
                _newMessageRequest.Sender = NewInstanceOf<Account>();

                _messageThread = NewInstanceOf<MessageThread>();

                Injected<IDateTime>().Stub(d => d.UtcNow).Return(new DateTime(2016, 1, 2));
            };

            static NewMessageRequest _newMessageRequest;
            static MessageThread _messageThread;
            static Message _result;
        }
    }
}

