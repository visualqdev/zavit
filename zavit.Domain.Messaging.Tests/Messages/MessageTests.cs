using System;
using Machine.Specifications;
using Rhino.Mspec.Contrib;
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
    }
}

