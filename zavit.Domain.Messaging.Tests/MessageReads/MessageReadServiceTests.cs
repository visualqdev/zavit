using System;
using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Shared;

namespace zavit.Domain.Messaging.Tests.MessageReads 
{
    [Subject("MessageReadService")]
    public class MessageReadServiceTests : TestOf<MessageReadService>
    {
        class When_being_told_that_messages_have_been_read_on_a_thread
        {
            Because of = () => Subject.MessagesRead(MessageThreadId, _account);

            It should_ask_the_message_read_processor_to_process_messages_that_the_user_has_not_read_yet =
                () => Injected<IMessageReadProcessor>().AssertWasCalled(p => p.Process(_unreadMessages, _account, DateRead));

            It should_notify_the_observers_that_some_of_the_messages_that_the_user_has_read_are_now_completely_read =
                () => _observers.ForEach(observer => observer.AssertWasCalled(o => o.MessagesRead(_completelyReadMessages, MessageThreadId)));

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 12;

                Injected<IDateTime>().Stub(d => d.UtcNow).Return(DateRead);

                var message = NewInstanceOf<Message>();
                message.Id = 1234;
                var otherMessage = NewInstanceOf<Message>();
                otherMessage.Id = 4567;

                _unreadMessages = new[] { message, otherMessage };
                Injected<IMessageReadRepository>()
                    .Stub(r => r.UnreadMessagesByUser(MessageThreadId, _account.Id, DateRead))
                    .Return(_unreadMessages);

                _completelyReadMessages = new List<int> { 1234 };
                Injected<IMessageReadRepository>()
                    .Stub(r => r.GetReadMessageIds(
                        Arg<int>.Is.Equal(MessageThreadId),
                        Arg<IEnumerable<int>>.List.Equal(new[] { message.Id, otherMessage.Id })))
                    .Return(_completelyReadMessages);

                _observers = (List<IMessageReadObserver>)Injected<IEnumerable<IMessageReadObserver>>();
                _observers.Add(NewInstanceOf<IMessageReadObserver>());
                _observers.Add(NewInstanceOf<IMessageReadObserver>());
            };

            static IList<Message> _unreadMessages;
            static readonly DateTime DateRead = new DateTime(2016, 09, 20);
            static IList<int> _completelyReadMessages;
            static List<IMessageReadObserver> _observers;
            static Account _account;
            static int MessageThreadId = 123;
        }
    }
}

