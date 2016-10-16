using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessages;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoServices.Messaging.MessageThreads.NewMessages 
{
    [Subject("NewMessageRequestProvider")]
    public class NewMessageRequestProviderTests : TestOf<NewMessageRequestProvider>
    {
        class When_providing_new_message_request
        {
            Because of = () => _result = Subject.Provide(_messageDto);

            It should_the_message_sender_to_be_the_current_user = () => _result.Sender.ShouldEqual(Injected<IUserContext>().Account);

            It should_set_the_body_to_be_the_message_dto_body = () => _result.Body.ShouldEqual(_messageDto.Body);

            Establish context = () =>
            {
                _messageDto = NewInstanceOf<MessageDto>();

                Injected<IUserContext>().Stub(c => c.Account).Return(NewInstanceOf<Account>());
            };

            static MessageDto _messageDto;
            static NewMessageRequest _result;
        }
    }
}

