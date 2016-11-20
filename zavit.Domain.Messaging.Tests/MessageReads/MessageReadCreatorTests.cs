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
            Because of = () => _result = Subject.Create(_account, _message);

            It should_set_the_user_to_be_the_porvided_user = () => _result.Account.ShouldEqual(_account);

            It should_set_the_message_to_be_the_porvided_message = () => _result.Message.ShouldEqual(_message);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _message = NewInstanceOf<Message>();
            };
            
            static Message _message;
            static Account _account;
            static MessageRead _result;
        }
    }
}

