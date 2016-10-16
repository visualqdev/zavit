using System.Web.Http;
using System.Web.Http.Results;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("MessageThreadController")]
    public class MessageThreadControllerTests : TestOf<MessageThreadsController>
    {
        class When_adding_a_message_to_a_new_message_thread
        {
            Because of = () => _result = Subject.Post(_newMessageThreadDto);

            It should_return_http_result_specifying_the_default_route =
               () => ((CreatedAtRouteNegotiatedContentResult<NewMessageThreadDto>)_result).RouteName.ShouldEqual(MessageThreadsController.GetMessageThreadRoute);

            It should_return_http_result_with_a_newly_created_message_thread_dto =
                () => ((CreatedAtRouteNegotiatedContentResult<NewMessageThreadDto>)_result).Content.ShouldEqual(_createdNewmessageThreadDto);

            It should_return_http_result_specifying_the_created_venue_id_as_a_route_value =
                () => int.Parse(((CreatedAtRouteNegotiatedContentResult<NewMessageThreadDto>)_result).RouteValues["id"].ToString()).ShouldEqual(_createdNewmessageThreadDto.Thread.Id);

            Establish context = () =>
            {
                _newMessageThreadDto = NewInstanceOf<NewMessageThreadDto>();

                _createdNewmessageThreadDto = NewInstanceOf<NewMessageThreadDto>();
                _createdNewmessageThreadDto.Thread = NewInstanceOf<MessageThreadDto>();
                _createdNewmessageThreadDto.Thread.Id = 123;

                Injected<IMessageThreadDtoService>()
                    .Stub(s => s.SendMessageOnNewThread(_newMessageThreadDto))
                    .Return(_createdNewmessageThreadDto);
            };

            static NewMessageThreadDto _newMessageThreadDto;
            static IHttpActionResult _result;
            static NewMessageThreadDto _createdNewmessageThreadDto;
        }

        class When_getting_the_message_thread
        {
            Because of = () => _result = Subject.Get(ThreadId);

            It should_return_the_message_thread_dto = () => _result.ShouldEqual(_messageThreadDto);

            Establish context = () =>
            {
                _messageThreadDto = NewInstanceOf<MessageThreadDto>();
                Injected<IMessageThreadDtoService>().Stub(s => s.GetMessageThread(ThreadId)).Return(_messageThreadDto);
            };

            static MessageThreadDto _result;
            static MessageThreadDto _messageThreadDto;
            const int ThreadId = 1;
        }
    }
}

