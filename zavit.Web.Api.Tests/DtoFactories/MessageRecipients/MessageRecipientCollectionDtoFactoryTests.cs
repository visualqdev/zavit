using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Shared.ResultCollections;
using zavit.Web.Api.DtoFactories.MessageRecipients;
using zavit.Web.Api.Dtos.MessageRecipients;

namespace zavit.Web.Api.Tests.DtoFactories.MessageRecipients 
{
    [Subject("MessageRecipientCollectionDtoFactory")]
    public class MessageRecipientCollectionDtoFactoryTests : TestOf<MessageRecipientCollectionDtoFactory>
    {
        class When_creating_message_recipient_collection_dto
        {
            Because of = () => _result = Subject.CreateItem(_recipientResultCollection);

            It should_set_the_has_more_result_to_be_the_same_as_result_collection = 
                () => _result.HasMoreResults.ShouldEqual(_recipientResultCollection.HasMoreResults);

            It should_create_recipients_dto_for_every_account_in_result_collection =
                () => _result.Recipients.ShouldContainOnly(_recipient, _otherRecipient);

            Establish context = () =>
            {
                _recipientResultCollection = NewInstanceOf<IResultCollection<Account>>();
                _recipientResultCollection.Stub(r => r.HasMoreResults).Return(true);

                var account = NewInstanceOf<Account>();
                var otherAccount = NewInstanceOf<Account>();
                _recipientResultCollection.Stub(a => a.Results).Return(new[] {account, otherAccount});

                _recipient = NewInstanceOf<MessageRecipientDto>();
                Injected<IMessageRecipientDtoFactory>().Stub(f => f.CreateItem(account)).Return(_recipient);

                _otherRecipient = NewInstanceOf<MessageRecipientDto>();
                Injected<IMessageRecipientDtoFactory>().Stub(f => f.CreateItem(otherAccount)).Return(_otherRecipient);
            };

            static IResultCollection<Account> _recipientResultCollection;
            static MessageRecipientCollectionDto _result;
            static MessageRecipientDto _recipient;
            static MessageRecipientDto _otherRecipient;
        }
    }
}

