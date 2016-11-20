using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Web.Api.DtoFactories.MessageRecipients;
using zavit.Web.Api.Dtos.MessageRecipients;
using zavit.Web.Api.DtoServices.MessageRecipients;

namespace zavit.Web.Api.Tests.DtoServices.MessageRecipients 
{
    [Subject("MessageRecipientDtoService")]
    public class MessageRecipientDtoServiceTests : TestOf<MessageRecipientDtoService>
    {
        class When_getting_recipients
        {
            Because of = () => _result = Subject.GetRecipients(_accountIds);

            It should_return_message_recipient_dto_for_every_account = () => _result.ShouldContainOnly(_messageRecipientDto, _otherMessageRecipientDto);

            Establish context = () =>
            {
                _accountIds = new[] {1, 2};

                var account = NewInstanceOf<Account>();
                var otherAccount = NewInstanceOf<Account>();
                Injected<IAccountRepository>()
                    .Stub(r => r.GetAccounts(_accountIds))
                    .Return(new[] {account, otherAccount});

                _messageRecipientDto = NewInstanceOf<MessageRecipientDto>();
                Injected<IMessageRecipientDtoFactory>().Stub(f => f.CreateItem(account)).Return(_messageRecipientDto);

                _otherMessageRecipientDto = NewInstanceOf<MessageRecipientDto>();
                Injected<IMessageRecipientDtoFactory>().Stub(f => f.CreateItem(otherAccount)).Return(_otherMessageRecipientDto);
            };

            static IEnumerable<int> _accountIds;
            static IEnumerable<MessageRecipientDto> _result;
            static MessageRecipientDto _messageRecipientDto;
            static MessageRecipientDto _otherMessageRecipientDto;
        }
    }
}

