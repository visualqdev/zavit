using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Shared.ResultCollections;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Api.DtoServices.Messaging.Messages;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessages;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoServices.Messaging.Messages 
{
    [Subject("MessageDtoService")]
    public class MessageDtoServiceTests : TestOf<MessageDtoService>
    {
        class When_sending_a_message_on_a_thread
        {
            Because of = () => _result = Subject.SendMessage(MessageThreadId, _messageDto);

            It should_return_a_message_dto_created_from_the_sent_message = () => _result.ShouldEqual(_sentMessage);

            Establish context = () =>
            {
                _messageDto = NewInstanceOf<MessageDto>();

                var newMessageRequest = NewInstanceOf<NewMessageRequest>();
                Injected<INewMessageRequestProvider>().Stub(p => p.Provide(_messageDto)).Return(newMessageRequest);

                var message = NewInstanceOf<Message>();
                Injected<IMessageService>().Stub(s => s.SendMessageOnThread(newMessageRequest, MessageThreadId)).Return(message);

                _sentMessage = NewInstanceOf<MessageDto>();
                Injected<IMessageDtoFactory>().Stub(f => f.CreateItem(message)).Return(_sentMessage);
            };

            static MessageDto _result;
            const int MessageThreadId = 123;
            static MessageDto _messageDto;
            static MessageDto _sentMessage;
        }

        class When_getting_messages_collection
        {
            Because of = () => _result = Subject.GetMessages(MessageThreadId, OlderThanMessageThread, Take);

            It should_return_message_collection_dto = () => _result.ShouldEqual(_messageCollectionDto);

            Establish context = () =>
            {
                var account = NewInstanceOf<Account>();
                Injected<IUserContext>().Stub(c => c.Account).Return(account);

                var messageCollection = NewInstanceOf<IResultCollection<Message>>();

                Injected<IMessageService>()
                    .Stub(s => s.GetMessages(MessageThreadId, OlderThanMessageThread, Take, account))
                    .Return(messageCollection);

                _messageCollectionDto = NewInstanceOf<MessagesCollectionDto>();
                Injected<IMessageCollectionDtoFactory>()
                    .Stub(f => f.CreateItem(messageCollection))
                    .Return(_messageCollectionDto);
            };

            static MessagesCollectionDto _result;
            const int Take = 2;
            static readonly int? OlderThanMessageThread = 456;
            static MessagesCollectionDto _messageCollectionDto;
            const int MessageThreadId = 123;
        }
    }
}

