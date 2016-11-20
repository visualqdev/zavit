using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using Rhino.Mspec.Contrib.Extensions;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Domain.Shared.ResultCollections;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessages;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessageThreads;
using zavit.Web.Core.Context;

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

        class When_getting_message_thread
        {
            Because of = () => _result = Subject.GetMessageThread(ThreadId, Take);

            It should_return_inbox_thread_details_dto_of_message_thread = () => _result.ShouldEqual(_messageThreadDto);

            Establish context = () =>
            {
                var messageThread = NewInstanceOf<MessageThread>();
                Injected<IMessageThreadService>().Stub(s => s.GetMessageThread(ThreadId)).Return(messageThread);

                Injected<IUserContext>().Stub(c => c.Account).Return(NewInstanceOf<Account>());

                var messageResults = NewInstanceOf<IResultCollection<MessageInfo>>();
                Injected<IMessageService>()
                    .Stub(m => m.GetMessages(ThreadId, null, Take, Injected<IUserContext>().Account))
                    .Return(messageResults);

                _messageThreadDto = NewInstanceOf<InboxThreadDetailsDto>();
                Injected<IInboxThreadDetailsDtoFactory>().Stub(f => f.CreateItem(messageThread, messageResults)).Return(_messageThreadDto);
            };

            static InboxThreadDetailsDto _result;
            static InboxThreadDetailsDto _messageThreadDto;
            const int Take = 20;
            const int ThreadId = 123;
        }

        class When_getting_message_threads
        {
            Because of = () => _result = Subject.GetMessageThreads();

            It should_return_message_thread_dto_for_each_message_thread = 
                () => _result.ShouldContainOnlyOrdered(_inboxThreadDto, _otherInboxThreadDto);

            Establish context = () =>
            {
                var account = NewInstanceOf<Account>();
                Injected<IUserContext>().Stub(c => c.Account).Return(account);

                var messageInbox = NewInstanceOf<MessageInbox>();
                var messageThread = NewInstanceOf<MessageThread>();
                var otherMessageThread = NewInstanceOf<MessageThread>();
                messageInbox.Threads = new[] { messageThread, otherMessageThread };
                Injected<IMessageThreadService>()
                    .Stub(s => s.GetMessageInbox(account))
                    .Return(messageInbox);

                _inboxThreadDto = NewInstanceOf<InboxThreadDto>();
                Injected<IInboxThreadDtoFactory>().Stub(f => f.CreateItem(messageThread, messageInbox)).Return(_inboxThreadDto);

                _otherInboxThreadDto = NewInstanceOf<InboxThreadDto>();
                Injected<IInboxThreadDtoFactory>().Stub(f => f.CreateItem(otherMessageThread, messageInbox)).Return(_otherInboxThreadDto);
            };

            static IEnumerable<InboxThreadDto> _result;
            static InboxThreadDto _inboxThreadDto;
            static InboxThreadDto _otherInboxThreadDto;
        }
    }
}

