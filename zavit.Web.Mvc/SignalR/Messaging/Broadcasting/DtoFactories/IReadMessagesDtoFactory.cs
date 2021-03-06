﻿using System.Collections.Generic;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories
{
    public interface IReadMessagesDtoFactory
    {
        ReadMessagesDto CreateItem(IList<Message> completelyReadMessages);
    }
}