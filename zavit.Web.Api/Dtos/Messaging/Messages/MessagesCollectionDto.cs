using System.Collections.Generic;

namespace zavit.Web.Api.Dtos.Messaging.Messages
{
    public class MessagesCollectionDto
    {
        public bool HasMoreResults { get; set; }
        public int Take { get; set; }
        public IEnumerable<MessageDto> Messages { get; set; }
    }
}