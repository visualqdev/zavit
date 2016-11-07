using System;
using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;

namespace zavit.Web.Mvc.Tests.SignalR.Messaging.Boradcasting.DtoFactorie 
{
    [Subject("ReadMessagesDtoFactory")]
    public class ReadMessagesDtoFactoryTests : TestOf<ReadMessagesDtoFactory>
    {
        class When_creating_the_read_message_dto
        {
            Because of = () => _result = Subject.CreateItem(_completelyReadMessages);

            It should_set_the_read_messages_stamps_to_be_stamps_of_all_messages = 
                () => _result.ReadMessageStamps.ShouldContainOnly(_message.Stamp, _otherMessage.Stamp);

            Establish context = () =>
            {
                _message = NewInstanceOf<Message>();
                _message.Stamp = Guid.NewGuid();
                _otherMessage = NewInstanceOf<Message>();
                _otherMessage.Stamp = Guid.NewGuid();

                _completelyReadMessages = new List<Message> { _message, _otherMessage };
            };

            static IList<Message> _completelyReadMessages;
            static ReadMessagesDto _result;
            static Message _message;
            static Message _otherMessage;
        }
    }
}

