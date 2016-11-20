using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Messaging.MessageThreads;

namespace zavit.Domain.Messaging.Tests.MessageThreads 
{
    [Subject("MessageInbox")]
    public class MessageInboxTests : TestOf<MessageInbox>
    {
        class When_getting_count_of_unread_messages_for_a_thread
        {
            Because of = () => _result = Subject.UnreadMessageCount(MessageThreadId);

            It should_return_the_count_stored_against_the_message_thread_id = () => _result.ShouldEqual(UnreadMessageCount);

            Establish context = () =>
            {
                Subject.UnreadMessageCountsPerThread = new Dictionary<int, int>
                {
                    { 1, 10 },
                    { MessageThreadId, UnreadMessageCount },
                    { 2, 11 }
                };
            };

            static int _result;
            const int UnreadMessageCount = 20;
            const int MessageThreadId = 123;
        }

        class When_getting_count_of_unread_messages_for_a_thread_but_the_thread_has_no_unread_messages
        {
            Because of = () => _result = Subject.UnreadMessageCount(MessageThreadId);

            It should_return_zero = () => _result.ShouldEqual(0);

            Establish context = () =>
            {
                Subject.UnreadMessageCountsPerThread = new Dictionary<int, int>
                {
                    { 1, 10 },
                    { 2, 11 }
                };
            };

            static int _result;
            const int MessageThreadId = 123;
        }

        class When_getting_latest_message_sent_on_the_specific_thread
        {
            Because of = () => _result = Subject.GetLatestMessage(MessageThreadId);

            It should_return_the_message_stored_against_the_message_thread_id = () => _result.ShouldEqual(_message);

            Establish context = () =>
            {
                _message = NewInstanceOf<Message>();

                Subject.LatestMessagesPerThread = new Dictionary<int, Message>
                {
                    { 1, NewInstanceOf<Message>() },
                    { MessageThreadId, _message },
                    { 3, NewInstanceOf<Message>() }
                };
            };

            static Message _result;
            static Message _message;
            const int MessageThreadId = 123;
        }

        class When_getting_latest_message_sent_on_the_specific_thread_but_that_thread_does_not_have_latest_message
        {
            Because of = () => _result = Subject.GetLatestMessage(MessageThreadId);

            It should_return_null = () => _result.ShouldBeNull();

            Establish context = () =>
            {
                Subject.LatestMessagesPerThread = new Dictionary<int, Message>
                {
                    { 1, NewInstanceOf<Message>() },
                    { 3, NewInstanceOf<Message>() }
                };
            };

            static Message _result;
            const int MessageThreadId = 123;
        }
    }
}

