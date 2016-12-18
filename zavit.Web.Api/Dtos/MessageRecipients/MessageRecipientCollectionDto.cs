using System.Collections.Generic;

namespace zavit.Web.Api.Dtos.MessageRecipients
{
    public class MessageRecipientCollectionDto
    {
        public bool HasMoreResults { get; set; }

        public IEnumerable<MessageRecipientDto> Recipients { get; set; }
    }
}