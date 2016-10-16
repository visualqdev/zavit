﻿using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;

namespace zavit.Web.Api.DtoFactories.Messaging.Messages
{
    public interface IMessageDtoFactory
    {
        MessageDto CreateItem(Message message);
    }
}