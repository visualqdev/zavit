using System;
using System.Collections.Generic;
using System.Web.Http;
using zavit.Web.Api.Dtos.MessageRecipients;
using zavit.Web.Api.DtoServices.MessageRecipients;

namespace zavit.Web.Api.Controllers
{
    public class MessageRecipientsController : ApiController
    {
        readonly IMessageRecipientDtoService _messageRecipientDtoService;

        public MessageRecipientsController(IMessageRecipientDtoService messageRecipientDtoService)
        {
            _messageRecipientDtoService = messageRecipientDtoService;
        }

        [HttpGet]
        [Route("~/api/messagerecipients")]
        public IEnumerable<MessageRecipientDto> GetRecipients([FromUri] IEnumerable<int> accountIds)
        {
            return _messageRecipientDtoService.GetRecipients(accountIds);
        }
    }
}