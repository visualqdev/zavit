using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.MessageRecipients;
using zavit.Web.Api.DtoServices.MessageRecipients;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("MessageRecipientSuggestionsController")]
    public class MessageRecipientSuggestionsControllerTests : TestOf<MessageRecipientSuggestionsController>
    {
        class When_getting_message_recipient_suggestions
        {
            Because of = () => _result = Subject.SuggestRecipients(SearchTerm, Skip, Take);

            It should_return_suggestion_for_the_provided_criteria = 
                () => _result.ShouldEqual(_messageRecipientCollectionDto);

            Establish context = () =>
            {
                _messageRecipientCollectionDto = NewInstanceOf<MessageRecipientCollectionDto>();

                Injected<IMessageRecipientDtoService>()
                    .Stub(s => s.Suggest(SearchTerm, Skip, Take))
                    .Return(_messageRecipientCollectionDto);
            };

            static MessageRecipientCollectionDto _result;
            static MessageRecipientCollectionDto _messageRecipientCollectionDto;
            const string SearchTerm = "Test term";
            const int Skip = 2;
            const int Take = 5;
        }
    }
}

