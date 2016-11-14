using System;
using System.Collections.Generic;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos
{
    public class ReadMessagesDto
    {
        public IEnumerable<Guid> ReadMessageStamps { get; set; }
    }
}