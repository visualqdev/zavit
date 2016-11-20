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
            Because of = () => _result = Subject.Provide(_newMessageRequest);

            It should_set_the_message_body_to_match_the_request = () => _result.Body.ShouldEqual(_newMessageRequest.Body);

            It should_set_the_sender_to_be_the_sender_from_the_request = () => _result.Sender.ShouldEqual(_newMessageRequest.Sender);

            It should_set_the_sent_on_to_be_the_current_utc_date_and_time = () => _result.SentOn.ShouldEqual(Injected<IDateTime>().UtcNow);

            It should_set_the_stamp_to_be_the_stamp_from_the_request = () => _result.Stamp.ShouldEqual(_newMessageRequest.Stamp);

            Establish context = () =>
            {
                _newMessageRequest = NewInstanceOf<NewMessageRequest>();
                _newMessageRequest.Body = "Test body";
                _newMessageRequest.Sender = NewInstanceOf<Account>();
                _newMessageRequest.Stamp = Guid.NewGuid();

                Injected<IDateTime>().Stub(d => d.UtcNow).Return(new DateTime(2016, 1, 2));
            };

            static NewMessageRequest _newMessageRequest;
            static Message _result;
        }

        class When_providing_message_and_the_request_stamp_is_empty
        {
            Because of = () => _result = Subject.Provide(_newMessageRequest);

            It should_set_the_message_body_to_match_the_request = () => _result.Body.ShouldEqual(_newMessageRequest.Body);

            It should_set_the_sender_to_be_the_sender_from_the_request = () => _result.Sender.ShouldEqual(_newMessageRequest.Sender);

            It should_set_the_sent_on_to_be_the_current_utc_date_and_time = () => _result.SentOn.ShouldEqual(Injected<IDateTime>().UtcNow);

            It should_set_the_stamp_to_be_a_new_guid = () => _result.Stamp.ShouldEqual(NewGuid);

            Establish context = () =>
            {
                _newMessageRequest = NewInstanceOf<NewMessageRequest>();
                _newMessageRequest.Body = "Test body";
                _newMessageRequest.Sender = NewInstanceOf<Account>();
                _newMessageRequest.Stamp = Guid.Empty;

                Injected<IDateTime>().Stub(d => d.UtcNow).Return(new DateTime(2016, 1, 2));
                Injected<IGuid>().Stub(g => g.NewGuid()).Return(NewGuid);
            };

            static readonly Guid NewGuid = Guid.NewGuid();
            static NewMessageRequest _newMessageRequest;
            static Message _result;
        }
    }
}

