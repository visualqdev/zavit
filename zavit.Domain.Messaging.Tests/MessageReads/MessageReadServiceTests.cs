using System;
using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Domain.Shared;

namespace zavit.Domain.Messaging.Tests.MessageReads 
{
    [Subject("MessageReadService")]
    public class MessageReadServiceTests : TestOf<MessageReadService>
    {
        class When_user_has_has_read_messages_on_messages_thread
        {
            Because of = () => Subject.MessagesRead(MessageThreadId, _account);

            It should_tell_all_pending_message_reads_that_they_have_been_read = () =>
            {
                _messageRead.AssertWasCalled(r => r.UserHasRead(_dateRead));
                _otherMessageRead.AssertWasCalled(r => r.UserHasRead(_dateRead));
            };

            It should_update_update_all_pending_message_reads = 
                () => Injected<IMessageReadRepository>().AssertWasCalled(r => r.Update(new[] { _messageRead, _otherMessageRead }));

            It should_notify_the_observers_that_some_of_the_messages_that_the_user_has_read_are_now_completely_read =
                () => _observers.ForEach(observer => observer.AssertWasCalled(o => o.MessagesRead(_completelyReadMessages, MessageThreadId)));

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;

                _dateRead = new DateTime(2016, 10, 30, 20, 57, 0);
                Injected<IDateTime>().Stub(d => d.UtcNow).Return(_dateRead);

                _messageRead = NewInstanceOf<MessageRead>();
                _messageRead.Message = NewInstanceOf<Message>();
                _messageRead.Message.Id = 10;

                _otherMessageRead = NewInstanceOf<MessageRead>();
                _otherMessageRead.Message = NewInstanceOf<Message>();
                _otherMessageRead.Message.Id = 11;

                Injected<IMessageReadRepository>()
                    .Stub(r => r.GetPendingMessageReads(MessageThreadId, _account.Id))
                    .Return(new List<MessageRead> { _messageRead, _otherMessageRead });

                _completelyReadMessages = new List<Message> { NewInstanceOf<Message>() };
                Injected<IMessageReadRepository>()
                    .Stub(r => r.GetReadMessages(
                        Arg<IEnumerable<int>>.List.Equal(new[] { _messageRead.Message.Id, _otherMessageRead.Message.Id })))
                    .Return(_completelyReadMessages);

                _observers = (List<IMessageReadObserver>)Injected<IEnumerable<IMessageReadObserver>>();
                _observers.Add(NewInstanceOf<IMessageReadObserver>());
                _observers.Add(NewInstanceOf<IMessageReadObserver>());
            };

            static Account _account;
            static int MessageThreadId = 123;
            static MessageRead _messageRead;
            static MessageRead _otherMessageRead;
            static DateTime _dateRead;
            static IList<Message> _completelyReadMessages;
            static List<IMessageReadObserver> _observers;
        }

        class When_message_has_been_sent_on_a_thread
        {
            Because of = () => Subject.MessageSent(_message);

            It should_save_all_message_reads_created_for_every_message_recipient = 
                () => Injected<IMessageReadRepository>().AssertWasCalled(r => r.Save(new[] { _messageRead, _otherMessageRead }));

            Establish context = () =>
            {
                var recipient = NewInstanceOf<Account>();
                var otherRecipient = NewInstanceOf<Account>();

                _message = NewInstanceOf<Message>();
                _message.Stub(m => m.GetRecipients()).Return(new[] { recipient, otherRecipient });

                _messageRead = NewInstanceOf<MessageRead>();
                Injected<IMessageReadCreator>().Stub(c => c.Create(recipient, _message)).Return(_messageRead);

                _otherMessageRead = NewInstanceOf<MessageRead>();
                Injected<IMessageReadCreator>().Stub(c => c.Create(otherRecipient, _message)).Return(_otherMessageRead);
            };

            static Message _message;
            static MessageRead _messageRead;
            static MessageRead _otherMessageRead;
        }
    }
}

