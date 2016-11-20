using System;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.Tests.DtoFactories.Messaging.MessageThreads 
{
    [Subject("InboxThreadDtoFactory")]
    public class InboxThreadDtoFactoryTests : TestOf<InboxThreadDtoFactory>
    {
        class When_creating_inbox_thread_dto
        {
            Because of = () => _result = Subject.CreateItem(_messageThread, _messageInbox);

            It should_set_the_title_to_be_the_title_built_by_message_thread_title_builder = 
                () => _result.ThreadTitle.ShouldEqual(ThreadTitle);

            It should_set_the_id_to_be_the_id_of_the_message_thread = () => _result.ThreadId.ShouldEqual(_messageThread.Id);

            It should_set_the_unread_message_count_to_the_value_specified_by_the_inbox_for_the_thread =
                () => _result.UnreadMessageCount.ShouldEqual(UnreadMessageCount);

            It should_set_the_latest_message_body_to_be_the_body_of_the_latest_message_on_a_thread =
                () => _result.LatestMessageBody.ShouldEqual(_latesMessage.Body);

            It should_set_the_latest_message_sent_on_date_to_be_the_sent_on_date_of_the_latest_message_on_a_thread =
                () => _result.LatestMessageSentOn.ShouldEqual(_latesMessage.SentOn);

            Establish context = () =>
            {
                _messageThread = NewInstanceOf<MessageThread>();
                _messageThread.Id = 123;

                _messageInbox = NewInstanceOf<IMessageInbox>();
                _messageInbox.Stub(i => i.AccountId).Return(123456);
                _messageInbox.Stub(i => i.UnreadMessageCount(_messageThread.Id)).Return(UnreadMessageCount);

                _latesMessage = NewInstanceOf<Message>();
                _latesMessage.Body = "Latest message body";
                _latesMessage.SentOn = new DateTime(2016, 10, 26, 6, 40, 0);

                _messageInbox.Stub(i => i.GetLatestMessage(_messageThread.Id)).Return(_latesMessage);

                Injected<IMessageThreadTitleBuilder>().Stub(b => b.BuildTitle(_messageThread, _messageInbox.AccountId)).Return(ThreadTitle);
            };

            static MessageThread _messageThread;
            static IMessageInbox _messageInbox;
            static InboxThreadDto _result;
            static Message _latesMessage;
            const int UnreadMessageCount = 456;
            const string ThreadTitle = "Thread title";
        }
    }
}

