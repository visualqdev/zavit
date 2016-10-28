using System.Collections.Generic;
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
                () => int.Parse(((CreatedAtRouteNegotiatedContentResult<NewMessageThreadDto>)_result).RouteValues["id"].ToString()).ShouldEqual(_createdNewmessageThreadDto.Thread.ThreadId);

            Establish context = () =>
            {
                _newMessageThreadDto = NewInstanceOf<NewMessageThreadDto>();

                _createdNewmessageThreadDto = NewInstanceOf<NewMessageThreadDto>();
                _createdNewmessageThreadDto.Thread = NewInstanceOf<MessageThreadDto>();
                _createdNewmessageThreadDto.Thread.ThreadId = 123;

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
            Because of = () => _result = Subject.Get(ThreadId, MessagesTake);

            It should_return_the_inbox_thread_details_dto = () => _result.ShouldEqual(_inboxThreadDetailsDto);

            Establish context = () =>
            {
                _inboxThreadDetailsDto = NewInstanceOf<InboxThreadDetailsDto>();
                Injected<IMessageThreadDtoService>().Stub(s => s.GetMessageThread(ThreadId, MessagesTake)).Return(_inboxThreadDetailsDto);
            };

            static InboxThreadDetailsDto _result;
            static InboxThreadDetailsDto _inboxThreadDetailsDto;
            const int ThreadId = 1;
            const int MessagesTake = 25;
        }

        class When_getting_message_threads_collection
        {
            Because of = () => _result = Subject.Get();

            It should_return_collection_of_inbox_threads = () => _result.ShouldEqual(_inboxThreads);

            Establish context = () =>
            {
                _inboxThreads = new[] { NewInstanceOf<InboxThreadDto>(), NewInstanceOf<InboxThreadDto>() };
                Injected<IMessageThreadDtoService>()
                    .Stub(s => s.GetMessageThreads())
                    .Return(_inboxThreads);
            };

            static IEnumerable<InboxThreadDto> _result;
            static IEnumerable<InboxThreadDto> _inboxThreads;
        }
    }
}

