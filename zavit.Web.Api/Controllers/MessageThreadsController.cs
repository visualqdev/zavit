using System.Collections.Generic;
using System.Web.Http;
using zavit.Web.Api.Authorization.AccessAuthorization;
using zavit.Web.Api.Authorization.ResourcesAuthorization.Messaging;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads;

namespace zavit.Web.Api.Controllers
{
    public class MessageThreadsController : ApiController
    {
        public const string GetMessageThreadRoute = "getMessageThreadRoute";

        readonly IMessageThreadDtoService _messageThreadDtoService;

        public MessageThreadsController(IMessageThreadDtoService messageThreadDtoService)
        {
            _messageThreadDtoService = messageThreadDtoService;
        }

        [HttpPost]
        [AccessAuthorize]
        [Route("~/api/messagethreads/new")]
        public IHttpActionResult Post(NewMessageThreadDto newMessageThreadDto)
        {
            var newMessageThread = _messageThreadDtoService.SendMessageOnNewThread(newMessageThreadDto);
            return CreatedAtRoute(GetMessageThreadRoute, new {id = newMessageThread.Thread.ThreadId}, newMessageThread);
        }

        [HttpGet]
        [MessageThreadAccessAuthorize("id")]
        [Route("~/api/messagethreads/{id}", Name = GetMessageThreadRoute)]
        public InboxThreadDetailsDto Get(int id, int take = 20)
        {
            return _messageThreadDtoService.GetMessageThread(id, take);
        }

        [HttpGet]
        [AccessAuthorize]
        [Route("~/api/messagethreads")]
        public IEnumerable<InboxThreadDto> Get()
        {
            return _messageThreadDtoService.GetMessageThreads();
        }
    }
}