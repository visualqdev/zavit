using System.Collections.Generic;
using zavit.Web.Api.Dtos.MessageRecipients;

namespace zavit.Web.Api.DtoServices.MessageRecipients
{
    public interface IMessageRecipientDtoService
    {
        IEnumerable<MessageRecipientDto> GetRecipients(IEnumerable<int> accountIds);
        MessageRecipientCollectionDto Suggest(string searchTerm, int skip, int take);
    }
}