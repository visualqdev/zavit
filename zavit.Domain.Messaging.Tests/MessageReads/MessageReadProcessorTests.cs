using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.Tests.MessageReads
{
    [Subject("MessageReadProcessor")]
    public class MessageReadProcessorTests : TestOf<MessageReadProcessor>
    {
        class When_processing_unread_messages_which_have_been_accessed_by_a_user
        {
            Because of = () => Subject.Process(_unreadMessages, _account, _dateRead);

            It should_save_every_new_instant_message_read_instance_except_the_ones_that_were_sent_by_user =
                () => Injected<IMessageReadRepository>()
                    .AssertWasCalled(r => r.Save(Arg<IEnumerable<MessageRead>>
                            .Matches(m => m.First() == _instantMessageRead && m.Last() == _otherInstantMessageRead)));

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;

                var message = NewInstanceOf<Message>();
                message.Sender = NewInstanceOf<Account>();

                var otherMessage = NewInstanceOf<Message>();
                otherMessage.Sender = NewInstanceOf<Account>();

                var messageSentByUser = NewInstanceOf<Message>();
                messageSentByUser.Sender = NewInstanceOf<Account>();
                messageSentByUser.Sender.Id = _account.Id;

                _unreadMessages = new List<Message> { message, otherMessage, messageSentByUser };

                _instantMessageRead = NewInstanceOf<MessageRead>();
                Injected<IMessageReadCreator>().Stub(c => c.Create(_account, message, _dateRead)).Return(_instantMessageRead);

                _otherInstantMessageRead = NewInstanceOf<MessageRead>();
                Injected<IMessageReadCreator>().Stub(c => c.Create(_account, otherMessage, _dateRead)).Return(_otherInstantMessageRead);
            };

            static IList<Message> _unreadMessages;
            static readonly DateTime _dateRead = new DateTime(2016, 09, 20);
            static MessageRead _instantMessageRead;
            static MessageRead _otherInstantMessageRead;
            static Account _account;
        }
    }
}