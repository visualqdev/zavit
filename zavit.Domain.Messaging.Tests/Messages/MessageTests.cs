using System;
using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.Tests.Messages 
{
    [Subject("Message")]
    public class MessageTests : TestOf<Message>
    {
        class When_being_added_to_a_thread
        {
            Because of = () => Subject.AddToThread(_messageThread);

            It should_set_the_messages_thread_to_be_the_specified_message_thread = 
                () => Subject.MessageThread.ShouldEqual(_messageThread);

            It should_set_the_las_updated_date_of_message_thread_to_be_the_sent_on_date =
                () => _messageThread.LastUpdatedOn.ShouldEqual(Subject.SentOn);

            Establish context = () =>
            {
                Subject.SentOn = new DateTime(2016, 10, 24, 21, 13, 0);

                _messageThread = NewInstanceOf<MessageThread>();
            };

            static MessageThread _messageThread;
        }

        class When_getting_reicpients
        {
            Because of = () => _result = Subject.GetRecipients();

            It should_return_all_thread_participants_except_the_message_sender = 
                () => _result.ShouldContainOnly(_recipient, _otherRecipient);

            Establish context = () =>
            {
                Subject.Sender = NewInstanceOf<Account>();
                Subject.Sender.Id = 123;

                _recipient = NewInstanceOf<Account>();
                _recipient.Id = 234;

                _otherRecipient = NewInstanceOf<Account>();
                _otherRecipient.Id = 345;

                var sender = NewInstanceOf<Account>();
                sender.Id = Subject.Sender.Id;

                Subject.MessageThread = NewInstanceOf<MessageThread>();
                Subject.MessageThread.Participants = new List<Account> { _recipient, _otherRecipient, sender };
            };

            static IEnumerable<Account> _result;
            static Account _recipient;
            static Account _otherRecipient;
        }
    }
}

