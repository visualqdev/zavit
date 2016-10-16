using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessages;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessageThreads;

namespace zavit.Web.Api.Tests.DtoServices.Messaging.MessageThreads 
{
    [Subject("MessageThreadDtoService")]
    public class MessageThreadDtoServiceTests : TestOf<MessageThreadDtoService>
    {
        class When_sending_a_message_on_a_new_message_thread
        {
            Because of = () => _result = Subject.SendMessageOnNewThread(_newMessageThreadDto);

            It should_return_a_newly_created_message_thread_dto = () => _result.ShouldEqual(_createdNewMessageThreadDto);

            Establish context = () =>
            {
                _newMessageThreadDto = NewInstanceOf<NewMessageThreadDto>();
                _newMessageThreadDto.Thread = NewInstanceOf<MessageThreadDto>();
                _newMessageThreadDto.Message = NewInstanceOf<MessageDto>();

                var newMessageThreadRequest = NewInstanceOf<NewMessageThreadRequest>();
                Injected<INewMessageThreadRequestProvider>()
                    .Stub(p => p.Provide(_newMessageThreadDto.Thread))
                    .Return(newMessageThreadRequest);

                var messageThread = NewInstanceOf<MessageThread>();
                Injected<IMessageThreadService>().Stub(s => s.CreateNewThread(newMessageThreadRequest)).Return(messageThread);

                var newMessageRequest = NewInstanceOf<NewMessageRequest>();
                Injected<INewMessageRequestProvider>()
                    .Stub(p => p.Provide(_newMessageThreadDto.Message))
                    .Return(newMessageRequest);

                var message = NewInstanceOf<Message>();
                Injected<IMessageService>()
                    .Stub(s => s.SendMessageOnThread(newMessageRequest, messageThread))
                    .Return(message);

                _createdNewMessageThreadDto = NewInstanceOf<NewMessageThreadDto>();
                Injected<INewMessageThreadDtoFactory>().Stub(f => f.CreateItem(messageThread, message)).Return(_createdNewMessageThreadDto);
            };

            static NewMessageThreadDto _newMessageThreadDto;
            static NewMessageThreadDto _result;
            static NewMessageThreadDto _createdNewMessageThreadDto;
        }
    }
}

