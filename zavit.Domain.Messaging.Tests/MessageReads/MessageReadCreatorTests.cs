using System;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.Tests.MessageReads 
{
    [Subject("MessageReadCreator")]
    public class MessageReadCreatorTests : TestOf<MessageReadCreator>
    {
        class When_creating_an_instant_message_read
        {
            Because of = () => _result = Subject.Create(_account, _message, DateRead);

            It should_set_the_user_to_be_the_porvided_user = () => _result.Account.ShouldEqual(_account);

            It should_set_the_message_to_be_the_porvided_message = () => _result.Message.ShouldEqual(_message);

            It should_set_the_date_read_to_be_the_porvided_date_read = () => _result.DateRead.ShouldEqual(DateRead);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _message = NewInstanceOf<Message>();
            };

            static readonly DateTime DateRead = new DateTime(2016, 9, 20);
            static Message _message;
            static Account _account;
            static MessageRead _result;
        }
    }
}

