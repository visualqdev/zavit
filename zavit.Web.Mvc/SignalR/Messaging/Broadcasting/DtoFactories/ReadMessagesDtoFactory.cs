using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories
{
    public class ReadMessagesDtoFactory : IReadMessagesDtoFactory
    {
        public ReadMessagesDto CreateItem(IList<Message> completelyReadMessages)
        {
            return new ReadMessagesDto
            {
                ReadMessageStamps = completelyReadMessages.Select(m => m.Stamp)
            };
        }
    }
}