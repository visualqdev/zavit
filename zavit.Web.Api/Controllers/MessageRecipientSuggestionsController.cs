using System.Web.Http;
using zavit.Web.Api.Authorization.AccessAuthorization;
using zavit.Web.Api.Dtos.MessageRecipients;
using zavit.Web.Api.DtoServices.MessageRecipients;

namespace zavit.Web.Api.Controllers
{
    public class MessageRecipientSuggestionsController : ApiController
    {
        readonly IMessageRecipientDtoService _messageRecipientDtoService;

        public MessageRecipientSuggestionsController(IMessageRecipientDtoService messageRecipientDtoService)
        {
            _messageRecipientDtoService = messageRecipientDtoService;
        }

        [HttpGet]
        [Route("~/api/messagerecipientsuggestions")]
        [AccessAuthorize]
        public MessageRecipientCollectionDto SuggestRecipients(string searchTerm, int skip = 0, int take = 20)
        {
            return _messageRecipientDtoService.Suggest(searchTerm, skip, take);
        }
    }
}