using System;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging.MessageReads;

namespace zavit.Domain.Messaging.Tests.MessageReads 
{
    [Subject("MessageRead")]
    public class MessageReadTests : TestOf<MessageRead>
    {
        class When_user_has_read_a_message
        {
            Because of = () => Subject.UserHasRead(DateRead);

            It should_set_the_date_read_to_be_the_date_provided = () => Subject.DateRead.ShouldEqual(DateRead);

            It should_set_the_has_read_property_to_true = () => Subject.HasRead.ShouldBeTrue();

            Establish context = () => {};

            static readonly DateTime DateRead = new DateTime(2016, 10, 30, 21, 36, 0);
        }
    }
}

