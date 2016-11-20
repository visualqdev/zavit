using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Web.Api.DtoFactories.MessageRecipients;
using zavit.Web.Api.Dtos.MessageRecipients;

namespace zavit.Web.Api.Tests.DtoFactories.MessageRecipients 
{
    [Subject("MessageRecipientDtoFactory")]
    public class MessageRecipientDtoFactoryTests : TestOf<MessageRecipientDtoFactory>
    {
        class When_creating_message_recipient_dto
        {
            Because of = () => _result = Subject.CreateItem(_account);

            It should_set_the_display_name_to_be_the_same_as_account = 
                () => _result.DisplayName.ShouldEqual(_account.DisplayName);

            It should_set_the_account_id_to_be_the_same_as_account =
                () => _result.AccountId.ShouldEqual(_account.Id);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;
                _account.DisplayName = "Test display name";
            };

            static Account _account;
            static MessageRecipientDto _result;
        }
    }
}

