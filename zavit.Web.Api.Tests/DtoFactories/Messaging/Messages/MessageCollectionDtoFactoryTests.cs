using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Shared.ResultCollections;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;

namespace zavit.Web.Api.Tests.DtoFactories.Messaging.Messages 
{
    [Subject("MessageCollectionDtoFactory")]
    public class MessageCollectionDtoFactoryTests : TestOf<MessageCollectionDtoFactory>
    {
        class When_crating_message_collection_dto
        {
            Because of = () => _result = Subject.CreateItem(_messageCollection);

            It should_set_the_has_more_results_property_to_be_the_same_as_message_collection = 
                () => _result.HasMoreResults.ShouldEqual(_messageCollection.HasMoreResults);

            It should_set_the_take_value_to_be_the_same_as_message_collection =
                () => _result.Take.ShouldEqual(_messageCollection.Take);

            It should_create_a_message_dto_for_every_message_in_the_result_collection =
                () => _result.Messages.ShouldContainOnly(_messageDto, _otherMessageDto);

            Establish context = () =>
            {
                _messageCollection = NewInstanceOf<IResultCollection<Message>>();
                _messageCollection.Stub(c => c.HasMoreResults).Return(true);

                var message = NewInstanceOf<Message>();
                var otherMessage = NewInstanceOf<Message>();
                _messageCollection.Stub(c => c.Results).Return(new[] {message, otherMessage});

                _messageDto = NewInstanceOf<MessageDto>();
                Injected<IMessageDtoFactory>().Stub(f => f.CreateItem(message)).Return(_messageDto);

                _otherMessageDto = NewInstanceOf<MessageDto>();
                Injected<IMessageDtoFactory>().Stub(f => f.CreateItem(otherMessage)).Return(_otherMessageDto);
            };

            static IResultCollection<Message> _messageCollection;
            static MessagesCollectionDto _result;
            static MessageDto _messageDto;
            static MessageDto _otherMessageDto;
        }
    }
}

