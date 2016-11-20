using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.Tests.DtoFactories.Messaging.MessageThreads 
{
    [Subject("NewMessageThreadDtoFactory")]
    public class NewMessageThreadDtoFactoryTests : TestOf<NewMessageThreadDtoFactory>
    {
        class When_creating_new_message_thread_dto
        {
            Because of = () => _result = Subject.CreateItem(_messageThread, _message);

            It should_set_the_thread_property_to_the_new_message_thread_dto = 
                () => _result.Thread.ShouldEqual(_messageThreadDto);

            It should_set_the_message_property_to_the_new_message_dto = () => _result.Message.ShouldEqual(_messageDto);

            Establish context = () =>
            {
                _messageThread = NewInstanceOf<MessageThread>();
                _message = NewInstanceOf<Message>();

                _messageThreadDto = NewInstanceOf<MessageThreadDto>();
                Injected<IMessageThreadDtoFactory>().Stub(f => f.CreateItem(_messageThread)).Return(_messageThreadDto);

                _messageDto = NewInstanceOf<MessageDto>();
                Injected<IMessageDtoFactory>().Stub(f => f.CreateItem(_message)).Return(_messageDto);
            };

            static MessageThread _messageThread;
            static Message _message;
            static NewMessageThreadDto _result;
            static MessageDto _messageDto;
            static MessageThreadDto _messageThreadDto;
        }
    }
}

