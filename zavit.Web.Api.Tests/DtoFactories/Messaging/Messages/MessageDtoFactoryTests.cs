using System;
using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;

namespace zavit.Web.Api.Tests.DtoFactories.Messaging.Messages 
{
    [Subject("MessageDtoFactory")]
    public class MessageDtoFactoryTests : TestOf<MessageDtoFactory>
    {
        class When_creating_message_dto
        {
            Because of = () => _result = Subject.CreateItem(_message);

            It should_set_the_sent_on_date_to_be_the_same_as_message = () => _result.SentOn.ShouldEqual(_message.SentOn);

            It should_set_the_body_to_be_the_same_as_message = () => _result.Body.ShouldEqual(_message.Body);

            It should_set_the_id_to_be_the_same_as_message = () => _result.Id.ShouldEqual(_message.Id);

            Establish context = () =>
            {
                _message = NewInstanceOf<Message>();
                _message.Id = 123;
                _message.Body = "Test body";
                _message.SentOn = new DateTime(2016, 10, 15);
            };

            static Message _message;
            static MessageDto _result;
        }
    }
}

