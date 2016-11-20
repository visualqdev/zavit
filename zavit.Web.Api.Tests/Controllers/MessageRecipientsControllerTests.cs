using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.MessageRecipients;
using zavit.Web.Api.DtoServices.MessageRecipients;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("MessageRecipientsController")]
    public class MessageRecipientsControllerTests : TestOf<MessageRecipientsController>
    {
        class When_getting_recipients_by_their_account_ids
        {
            Because of = () => _result = Subject.GetRecipients(_accountIds);

            It should_return_message_recipient_dtos = () => _result.ShouldEqual(_messageRecipientDtos);

            Establish context = () =>
            {
                _accountIds = new[] {1, 2};

                _messageRecipientDtos = new[] {NewInstanceOf<MessageRecipientDto>(), NewInstanceOf<MessageRecipientDto>()};
                Injected<IMessageRecipientDtoService>()
                    .Stub(f => f.GetRecipients(_accountIds))
                    .Return(_messageRecipientDtos);
            };

            static IEnumerable<int> _accountIds;
            static IEnumerable<MessageRecipientDto> _result;
            static IEnumerable<MessageRecipientDto> _messageRecipientDtos;
        }
    }
}

