using System;
using System.Web.Http;
using zavit.Web.Api.Authorization.ResourcesAuthorization.Messaging;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Api.DtoServices.Messaging.Messages;

namespace zavit.Web.Api.Controllers
{
    public class MessagesController : ApiController
    {
        readonly IMessageDtoService _messageDtoService;
        public const string GetMessagesRoute = "GetMessagesRoute";

        public MessagesController(IMessageDtoService messageDtoService)
        {
            _messageDtoService = messageDtoService;
        }

        [HttpPost]
        [Route("~/api/messagethreads/{messageThreadId}/messages")]
        [MessageThreadAccessAuthorize("messageThreadId")]
        public IHttpActionResult Post(int messageThreadId, MessageDto messageDto)
        {
            var createdMessageDto = _messageDtoService.SendMessage(messageThreadId, messageDto);
            return CreatedAtRoute(GetMessagesRoute, new { messageThreadId }, createdMessageDto);
        }

        [HttpGet]
        [Route("~/api/messagethreads/{messageThreadId}/messages", Name = GetMessagesRoute)]
        [MessageThreadAccessAuthorize("messageThreadId")]
        public MessagesCollectionDto Get(int messageThreadId, int? olderThanMessageId = null, int take = 20)
        {
            return _messageDtoService.GetMessages(messageThreadId, olderThanMessageId, take);
        }
    }
}